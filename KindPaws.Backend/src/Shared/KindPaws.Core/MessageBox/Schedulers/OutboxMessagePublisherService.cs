using System.Text.Json;
using KindPaws.Core.MessageBox.Abstractions.Interfaces;
using KindPaws.Core.MessageBox.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace KindPaws.Core.MessageBox.Schedulers;

public class OutboxMessagePublisherService : IOutboxMessagePublisherService
{
    private readonly IOutBoxWriteDbContext _outBoxWriteDbContext;
    private readonly IPublisher _publisher;
    private readonly ILogger<OutboxMessagePublisherService> _logger;

    public OutboxMessagePublisherService(
        IOutBoxWriteDbContext outBoxWriteDbContext,
        ILogger<OutboxMessagePublisherService> logger,
        IPublisher publisher)
    {
        _outBoxWriteDbContext = outBoxWriteDbContext;
        _logger = logger;
        _publisher = publisher;
    }

    public async Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        var messages = await _outBoxWriteDbContext
            .OutBoxMessages
            .Where(outBoxMessage => outBoxMessage.ProcessedAt == null)
            .OrderBy(outBoxMessage => outBoxMessage.OccuredAt)
            .Take(100)
            .ToListAsync(cancellationToken);

        if (messages.Count == 0)
            return;

        var retryStrategyOptions = new RetryStrategyOptions()
        {
            ShouldHandle = new PredicateBuilder().Handle<Exception>(),
            BackoffType = DelayBackoffType.Exponential,
            MaxRetryAttempts = 3,
            Delay = TimeSpan.FromSeconds(3),
            OnRetry = retryArguments =>
            {
                _logger.LogCritical(
                    retryArguments.Outcome.Exception,
                    "[OutBox | MessagePublisherService] Current attempt: {attemptNumber}",
                    retryArguments.AttemptNumber);

                return ValueTask.CompletedTask;
            },
        };

        var pipeline = new ResiliencePipelineBuilder()
            .AddRetry(retryStrategyOptions)
            .Build();

        var tasks = messages.Select(outBoxMessage => ProcessMessageAsync(
            outBoxMessage,
            pipeline,
            cancellationToken));

        await Task.WhenAll(tasks);

        try
        {
            await _outBoxWriteDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[OutBox] Failed to save changes to the database.");
        }
    }

    private async Task ProcessMessageAsync(
        BoxMessage message,
        ResiliencePipeline pipeline,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var currentEvent = JsonSerializer.Deserialize(message.Payload, Type.GetType(message.Type)!)!;

            await pipeline.ExecuteAsync(async token =>
            {
                await _publisher.Publish(currentEvent, token);
                message.ProcessedAt = DateTimeOffset.UtcNow;
            }, cancellationToken);
        }
        catch (Exception ex)
        {
            message.Error = ex.Message;
            message.ProcessedAt = DateTimeOffset.UtcNow;
            _logger.LogError(ex, "Failed to process message ID: {MessageId}", message.Id);
        }
    }
}
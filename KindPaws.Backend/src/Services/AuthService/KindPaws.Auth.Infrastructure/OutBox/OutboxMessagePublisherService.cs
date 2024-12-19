using System.Text.Json;
using KindPaws.Auth.Infrastructure.DbContexts;
using KindPaws.SharedKernel.Others;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace KindPaws.Auth.Infrastructure.OutBox;

public class OutboxMessagePublisherService
{
    private readonly AuthWriteDbContext _authWriteDbContext;
    private readonly IPublisher _publisher;
    private readonly ILogger<OutboxMessagePublisherService> _logger;

    public OutboxMessagePublisherService(
        AuthWriteDbContext authWriteDbContext,
        ILogger<OutboxMessagePublisherService> logger,
        IPublisher publisher)
    {
        _authWriteDbContext = authWriteDbContext;
        _logger = logger;
        _publisher = publisher;
    }

    public async Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        var messages = await _authWriteDbContext
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
            await _authWriteDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[OutBox] Failed to save changes to the database.");
        }
    }

    private async Task ProcessMessageAsync(
        OutBoxMessage message,
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
using KindPaws.Core.OutBox.Abstractions;
using Quartz;

namespace KindPaws.Core.OutBox.Schedulers;

[DisallowConcurrentExecution]
public class OutboxMessagePublisherJob : IJob
{
    private readonly IOutboxMessagePublisherService _outboxMessagePublisherService;

    public OutboxMessagePublisherJob(IOutboxMessagePublisherService outboxMessagePublisherService)
    {
        _outboxMessagePublisherService = outboxMessagePublisherService;
    }


    public async Task Execute(IJobExecutionContext context)
    {
        await _outboxMessagePublisherService.ProcessAsync(context.CancellationToken);
    }
}
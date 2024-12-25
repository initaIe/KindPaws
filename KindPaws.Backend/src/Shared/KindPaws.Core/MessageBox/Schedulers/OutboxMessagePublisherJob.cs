using KindPaws.Core.MessageBox.Abstractions.Interfaces;
using Quartz;

namespace KindPaws.Core.MessageBox.Schedulers;

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
using Quartz;

namespace KindPaws.Auth.Infrastructure.OutBox;

[DisallowConcurrentExecution]
public class OutboxMessagePublisherJob : IJob
{
    private readonly OutboxMessagePublisherService _outboxMessagePublisherService;

    public OutboxMessagePublisherJob(OutboxMessagePublisherService outboxMessagePublisherService)
    {
        _outboxMessagePublisherService = outboxMessagePublisherService;
    }


    public async Task Execute(IJobExecutionContext context)
    {
        await _outboxMessagePublisherService.ProcessAsync(context.CancellationToken);
    }
}
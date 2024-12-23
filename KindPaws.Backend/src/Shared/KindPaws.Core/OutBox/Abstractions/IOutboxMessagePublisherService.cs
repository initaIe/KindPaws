namespace KindPaws.Core.OutBox.Abstractions;

public interface IOutboxMessagePublisherService
{
    Task ProcessAsync(CancellationToken cancellationToken = default);
}
namespace KindPaws.Core.MessageBox.Abstractions.Interfaces;

public interface IOutboxMessagePublisherService
{
    Task ProcessAsync(CancellationToken cancellationToken = default);
}
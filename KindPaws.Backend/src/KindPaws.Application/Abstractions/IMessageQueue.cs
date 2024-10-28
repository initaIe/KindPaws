namespace KindPaws.Application.Abstractions;

public interface IMessageQueue<TMessage>
{
    Task WriteAsync(TMessage deleteFilesData, CancellationToken cancellationToken = default);
    Task<TMessage> ReadAsync(CancellationToken cancellationToken = default);
}
using KindPaws.SharedKernel.DDD;

namespace KindPaws.Core.MessageBox.Abstractions.Interfaces;

public interface IGeneralBoxRepository
{
    Task AddInBoxMessagesAsync<T>(IEnumerable<T> messages, CancellationToken cancellationToken = default)
        where T : IEvent;

    Task AddOutBoxMessagesAsync<T>(IEnumerable<T> messages, CancellationToken cancellationToken = default)
        where T : IEvent;
}
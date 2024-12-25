using KindPaws.SharedKernel.DDD;

namespace KindPaws.Core.MessageBox.Abstractions.Interfaces;

public interface IOutBoxRepository
{
    Task AddRangeAsync<T>(IEnumerable<T> messages, CancellationToken cancellationToken = default)
        where T : IEvent;
}
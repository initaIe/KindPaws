using KindPaws.SharedKernel.DDD;

namespace KindPaws.Core.OutBox.Abstractions;

public interface IOutBoxRepository
{
    Task AddRangeAsync<T>(IEnumerable<T> messages, CancellationToken cancellationToken = default)
        where T : IEvent;
}
using KindPaws.SharedKernel.Others;

namespace KindPaws.Auth.Application.Abstractions;

public interface IOutBoxRepository
{
    Task AddRangeAsync<T>(IEnumerable<T> messages, CancellationToken cancellationToken = default)
        where T : IEvent;
}
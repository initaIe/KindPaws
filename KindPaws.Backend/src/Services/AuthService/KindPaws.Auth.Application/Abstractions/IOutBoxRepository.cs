using KindPaws.SharedKernel.Others;

namespace KindPaws.Auth.Application.Abstractions;

public interface IOutBoxRepository
{
    Task AddAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IEvent;
}
using KindPaws.Application.Abstractions.Markers;
using KindPaws.Domain.Shared.Others;

namespace KindPaws.Application.Abstractions;

public interface ICommandHandler<TResponse, in TCommand> where TCommand : ICommand
{
    Task<Result<TResponse, ErrorList>> HandleAsync(
        TCommand command,
        CancellationToken cancellationToken = default);
}
using KindPaws.Core.Abstractions.Markers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Core.Abstractions;

public interface ICommandHandler<TResponse, in TCommand> where TCommand : ICommand
{
    Task<Result<TResponse, ErrorList>> HandleAsync(
        TCommand command,
        CancellationToken cancellationToken = default);
}
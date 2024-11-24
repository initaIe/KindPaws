using KindPaws.Core.Abstractions.Markers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Core.Abstractions.Handlers;

public interface IQueryHandler<TResponse, in TQuery> 
    where TQuery : IQuery
{
    Task<TResponse> HandleAsync(
        TQuery query,
        CancellationToken cancellationToken = default);
}
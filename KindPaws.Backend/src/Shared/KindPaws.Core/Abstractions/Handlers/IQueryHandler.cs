using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Core.Abstractions.Handlers;

public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
{
    Task<TResponse> HandleAsync(
        TQuery query,
        CancellationToken cancellationToken = default);
}
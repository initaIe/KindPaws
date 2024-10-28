using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Abstractions;

public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
{
    Task<TResponse> HandleAsync(
        TQuery query,
        CancellationToken cancellationToken = default);
}
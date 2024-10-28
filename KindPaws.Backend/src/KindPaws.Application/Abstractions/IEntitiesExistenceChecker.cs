using KindPaws.Application.Abstractions.Markers;
using KindPaws.Domain.Shared.Others;

namespace KindPaws.Application.Abstractions;

public interface IEntitiesExistenceChecker<in TExistenceCheckData> where TExistenceCheckData : IExistenceCheckData
{
    Task<Result<Error>> CheckAsync(
        TExistenceCheckData checkData,
        CancellationToken cancellationToken);
}
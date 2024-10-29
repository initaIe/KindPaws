using KindPaws.Application.Abstractions.Markers;
using KindPaws.Domain.Shared;

namespace KindPaws.Application.Abstractions;

public interface IEntitiesExistenceValidator<in TExistenceCheckData>
    where TExistenceCheckData : IExistenceValidationData
{
    Task<Result<Error>> ValidateAsync(
        TExistenceCheckData checkData,
        CancellationToken cancellationToken);
}
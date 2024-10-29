using KindPaws.Application.Abstractions.Markers;
using KindPaws.Domain.Shared;

namespace KindPaws.Application.Abstractions;

public interface IEntitiesExistenceValidator<in TExistenceValdiationData>
    where TExistenceValdiationData : IExistenceValidationData
{
    Task<Result<Error>> ValidateAsync(
        TExistenceValdiationData existenceValidationData,
        CancellationToken cancellationToken);
}
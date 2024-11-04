using KindPaws.Core.Abstractions.Markers;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;

namespace KindPaws.Core.Abstractions;

public interface IEntitiesExistenceValidator<in TExistenceValidationData>
    where TExistenceValidationData : IExistenceValidationData
{
    Task<Result<Error>> ValidateAsync(
        TExistenceValidationData existenceValidationData,
        CancellationToken cancellationToken);
}
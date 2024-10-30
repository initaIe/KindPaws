namespace KindPaws.Application.Abstractions.EntitiesExistenceValidators;

public interface ISpecieExistenceValidator
{
    Task<bool> IsSpecieByIdExistsAsync(
        Guid specieId,
        CancellationToken cancellationToken);

    Task<bool> IsSpecieByNameExistsAsync(
        string name,
        CancellationToken cancellationToken);
}
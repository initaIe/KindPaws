namespace KindPaws.Application.Abstractions.EntitiesExistenceValidators;

public interface IBreedExistenceValidator
{
    Task<bool> IsBreedByIdExistsAsync(
        Guid breedId,
        CancellationToken cancellationToken);

    Task<bool> IsBreedByIdForSpecieByIdExistsAsync(
        Guid specieId,
        Guid breedId,
        CancellationToken cancellationToken);

    Task<bool> IsBreedByNameForSpecieByIdExistsAsync(
        Guid specieId,
        string breedName,
        CancellationToken cancellationToken);
}
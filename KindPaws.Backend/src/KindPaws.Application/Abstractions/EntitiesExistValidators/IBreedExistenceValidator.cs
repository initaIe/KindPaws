namespace KindPaws.Application.Abstractions.EntitiesExistValidators;

public interface IBreedExistenceValidator
{
    Task<bool> IsBreedWithIdExistsAsync(
        Guid breedId,
        CancellationToken cancellationToken);

    Task<bool> IsBreedWithIdExistsForSpecieWithIdAsync(
        Guid specieId,
        Guid breedId,
        CancellationToken cancellationToken);

    Task<bool> IsBreedWithNameExistsForSpecieWithIdAsync(
        Guid specieId,
        string breedName,
        CancellationToken cancellationToken);
}
namespace KindPaws.Species.Contracts;

public interface ISpeciesContract
{
    Task<bool> IsSpecieByIdExistsAsync(
        Guid specieId,
        CancellationToken cancellationToken = default);

    Task<bool> IsBreedByIdExistsAsync(
        Guid breedId,
        CancellationToken cancellationToken = default);

    Task<bool> IsBreedByIdForSpecieByIdExistsAsync(
        Guid breedId,
        Guid specieId,
        CancellationToken cancellationToken = default);
}
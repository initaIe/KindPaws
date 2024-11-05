namespace KindPaws.Species.Contracts;

public interface ISpeciesContract
{
    Task<bool> IsSpecieByIdExistsAsync(Guid specieId, CancellationToken cancellationToken);
    Task<bool> IsBreedByIdExistsAsync(Guid breedId, CancellationToken cancellationToken);
    Task<bool> IsBreedByIdForSpecieByIdExistsAsync(Guid breedId, Guid specieId, CancellationToken cancellationToken);
}
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Species.Application.Abstractions;

public interface ISpeciesLockService
{
    Task SetSpecieLockForUpdateAsync(
        SpecieId specieId,
        CancellationToken cancellationToken = default);

    Task SetBreedLockForUpdateAsync(
        BreedId breedId,
        CancellationToken cancellationToken = default);
}
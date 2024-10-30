namespace KindPaws.Application.Abstractions.EntitiesExistenceValidators;

public interface IPetExistenceValidator
{
    Task<bool> IsPetByIdExistsAsync(
        Guid petId,
        CancellationToken cancellationToken);

    Task<bool> IsPetBySpecieIdExistsAsync(
        Guid specieId,
        CancellationToken cancellationToken);

    Task<bool> IsPetByBreedIdExistsAsync(
        Guid breedId,
        CancellationToken cancellationToken);

    Task<bool> IsPetByIdForVolunteerByIdExistsAsync(
        Guid volunteerId,
        Guid petId,
        CancellationToken cancellationToken);
}
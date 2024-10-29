namespace KindPaws.Application.Abstractions.EntitiesExistValidators;

public interface IPetExistenceValidator
{
    Task<bool> IsPetWithIdExistsAsync(
        Guid petId,
        CancellationToken cancellationToken);

    Task<bool> IsPetWithIdExistsForVolunteerWithIdAsync(
        Guid volunteerId,
        Guid petId,
        CancellationToken cancellationToken);
    
    Task<bool> IsPetWithSpecieIdExistsAsync(
        Guid specieId,
        CancellationToken cancellationToken);
    
    Task<bool> IsPetWithBreedIdExistsAsync(
        Guid breedId,
        CancellationToken cancellationToken);
}
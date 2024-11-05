namespace KindPaws.Volunteers.Contracts;

public interface IVolunteersContract
{
    Task<bool> IsPetByBreedIdExistsAsync(Guid breedId, CancellationToken cancellationToken);
    Task<bool> IsPetBySpecieIdExistsAsync(Guid breedId, CancellationToken cancellationToken);
}
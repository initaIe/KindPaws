namespace KindPaws.Volunteers.Contracts;

public interface IVolunteersContract
{
    Task<bool> IsPetByBreedIdExistsAsync(
        Guid breedId,
        CancellationToken cancellationToken = default);

    Task<bool> IsPetBySpecieIdExistsAsync(
        Guid breedId,
        CancellationToken cancellationToken = default);
}
namespace KindPaws.Application.Abstractions.ExistValidators;

public interface IBreedExistValidator
{
    Task<bool> IsBreedByIdExistsAsync(Guid breedId, CancellationToken cancellationToken);
    Task<bool> IsBreedByNameExistsAsync(string name, CancellationToken cancellationToken);
}
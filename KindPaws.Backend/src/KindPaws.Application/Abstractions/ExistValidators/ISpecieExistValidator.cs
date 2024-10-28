namespace KindPaws.Application.Abstractions.ExistValidators;

public interface ISpecieExistValidator
{
    Task<bool> IsSpecieByNameExistsAsync(string name, CancellationToken cancellationToken);
    Task<bool> IsSpecieByIdExistsAsync(Guid specieId, CancellationToken cancellationToken);
}
namespace KindPaws.Application.Abstractions.EntitiesExistValidators;

public interface ISpecieExistenceValidator
{
    Task<bool> IsSpecieWithNameExistsAsync(string name, CancellationToken cancellationToken);
    Task<bool> IsSpecieWithIdExistsAsync(Guid specieId, CancellationToken cancellationToken);
}
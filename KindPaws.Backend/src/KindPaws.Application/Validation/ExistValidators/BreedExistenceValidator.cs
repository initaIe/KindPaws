using KindPaws.Application.Abstractions.EntitiesExistValidators;
using KindPaws.Application.Abstractions.IoC;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Application.Validation.ExistValidators;

public class BreedExistenceValidator : IBreedExistenceValidator
{
    private readonly IReadDbContext _readDbContext;

    public BreedExistenceValidator(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<bool> IsBreedWithIdExistsAsync(
        Guid breedId,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Breeds.AnyAsync(
            b => b.Id == breedId,
            cancellationToken);
    }

    public async Task<bool> IsBreedWithIdExistsForSpecieWithIdAsync(
        Guid specieId,
        Guid breedId,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Breeds.AnyAsync(
            b => b.SpecieId == specieId && b.Id == breedId,
            cancellationToken);
    }

    public async Task<bool> IsBreedWithNameExistsForSpecieWithIdAsync(
        Guid specieId,
        string breedName,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Breeds.AnyAsync(
            b => b.SpecieId == specieId && b.Name.Equals(breedName, StringComparison.CurrentCultureIgnoreCase),
            cancellationToken);
    }
}
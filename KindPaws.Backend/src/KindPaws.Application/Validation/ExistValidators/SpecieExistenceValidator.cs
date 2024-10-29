using KindPaws.Application.Abstractions.EntitiesExistValidators;
using KindPaws.Application.Abstractions.IoC;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Application.Validation.ExistValidators;

public class SpecieExistenceValidator : ISpecieExistenceValidator
{
    private readonly IReadDbContext _readDbContext;

    public SpecieExistenceValidator(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<bool> IsSpecieWithIdExistsAsync(Guid specieId, CancellationToken cancellationToken)
    {
        return await _readDbContext.Species.AnyAsync(
            s => s.Id == specieId, cancellationToken);
    }

    public async Task<bool> IsSpecieWithNameExistsAsync(string name, CancellationToken cancellationToken)
    {
        return await _readDbContext.Species.AnyAsync(
            s => s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase), cancellationToken);
    }
}
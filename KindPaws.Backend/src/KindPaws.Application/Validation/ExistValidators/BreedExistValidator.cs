using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Application.Validation.ExistValidators;

public class BreedExistValidator
{
    private readonly IReadDbContext _readDbContext;

    public BreedExistValidator(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<bool> IsBreedByNameExistsAsync(string name, CancellationToken cancellationToken)
    {
        return await _readDbContext.Breeds.AnyAsync(
            b => b.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase), cancellationToken);
    }
    
    public async Task<bool> IsBreedByIdExistsAsync(Guid breedId, CancellationToken cancellationToken)
    {
        return await _readDbContext.Breeds.AnyAsync(
            b => b.Id == breedId, cancellationToken);
    }
}
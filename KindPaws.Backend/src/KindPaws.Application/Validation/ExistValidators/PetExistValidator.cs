using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.ExistValidators;
using KindPaws.Application.Abstractions.IoC;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Application.Validation.ExistValidators;

public class PetExistValidator : IPetExistValidator
{
    private readonly IReadDbContext _readDbContext;

    public PetExistValidator(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<bool> IsPetByIdExists(Guid petId, CancellationToken cancellationToken)
    {
        return await _readDbContext.Pets.AnyAsync(
            p => p.Id == petId, cancellationToken);
    }
}
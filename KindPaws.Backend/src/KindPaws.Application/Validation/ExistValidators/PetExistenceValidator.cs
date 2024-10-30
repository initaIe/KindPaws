using KindPaws.Application.Abstractions.EntitiesExistenceValidators;
using KindPaws.Application.Abstractions.IoC;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Application.Validation.ExistValidators;

public class PetExistenceValidator : IPetExistenceValidator
{
    private readonly IReadDbContext _readDbContext;

    public PetExistenceValidator(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<bool> IsPetByIdExistsAsync(
        Guid petId,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Pets.AnyAsync(
            p => p.Id == petId, cancellationToken);
    }

    public async Task<bool> IsPetByIdForVolunteerByIdExistsAsync(
        Guid volunteerId,
        Guid petId,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Pets.AnyAsync(
            p => p.VolunteerId == volunteerId && p.Id == petId, cancellationToken);
    }

    public async Task<bool> IsPetBySpecieIdExistsAsync(
        Guid specieId,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Pets.AnyAsync(
            p => p.SpecieId == specieId, cancellationToken);
    }

    public async Task<bool> IsPetByBreedIdExistsAsync(
        Guid breedId,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Pets.AnyAsync(
            p => p.BreedId == breedId, cancellationToken);
    }
}
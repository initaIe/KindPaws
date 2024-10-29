using KindPaws.Application.Abstractions.EntitiesExistValidators;
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

    public async Task<bool> IsPetWithIdExistsAsync(
        Guid petId,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Pets.AnyAsync(
            p => p.Id == petId, cancellationToken);
    }

    public async Task<bool> IsPetWithIdExistsForVolunteerWithIdAsync(
        Guid volunteerId,
        Guid petId,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Pets.AnyAsync(
            p => p.VolunteerId == volunteerId && p.Id == petId, cancellationToken);
    }

    public async Task<bool> IsPetWithSpecieIdExistsAsync(
        Guid specieId, 
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Pets.AnyAsync(
            p => p.SpecieId == specieId, cancellationToken);
    }

    public async Task<bool> IsPetWithBreedIdExistsAsync(
        Guid breedId,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Pets.AnyAsync(
            p => p.BreedId == breedId, cancellationToken);
    }
}
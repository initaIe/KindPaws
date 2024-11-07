using KindPaws.Species.Application.Interfaces;
using KindPaws.Species.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Species.Presentation;

public class SpeciesContract : ISpeciesContract
{
    private readonly ISpeciesReadDbContext _readDbContext;

    public SpeciesContract(ISpeciesReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public Task<bool> IsSpecieByIdExistsAsync(Guid specieId, CancellationToken cancellationToken = default)
    {
        return _readDbContext.Species.AnyAsync(s => s.Id == specieId, cancellationToken);
    }

    public Task<bool> IsBreedByIdExistsAsync(Guid breedId, CancellationToken cancellationToken = default)
    {
        return _readDbContext.Breeds.AnyAsync(b => b.Id == breedId, cancellationToken);
    }

    public Task<bool> IsBreedByIdForSpecieByIdExistsAsync(Guid breedId, Guid specieId,
        CancellationToken cancellationToken = default)
    {
        return _readDbContext.Breeds.AnyAsync(b => b.Id == breedId && b.SpecieId == specieId, cancellationToken);
    }
}
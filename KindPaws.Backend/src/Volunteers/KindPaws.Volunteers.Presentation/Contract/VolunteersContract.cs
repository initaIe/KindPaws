using KindPaws.Volunteers.Application.Abstractions;
using KindPaws.Volunteers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Presentation.Contract;

public class VolunteersContract : IVolunteersContract
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public VolunteersContract(IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public Task<bool> IsPetByBreedIdExistsAsync(Guid breedId, CancellationToken cancellationToken = default)
    {
        return _readDbContext.Pets.AnyAsync(p => p.BreedId == breedId, cancellationToken);
    }

    public Task<bool> IsPetBySpecieIdExistsAsync(Guid specieId, CancellationToken cancellationToken = default)
    {
        return _readDbContext.Pets.AnyAsync(p => p.SpecieId == specieId, cancellationToken);
    }
}
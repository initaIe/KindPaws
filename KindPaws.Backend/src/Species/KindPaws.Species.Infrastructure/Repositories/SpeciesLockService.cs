using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Application.Abstractions;
using KindPaws.Species.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Species.Infrastructure.Repositories;

public class SpeciesLockService : ISpeciesLockService
{
    private readonly SpeciesWriteDbContext _dbContext;

    public SpeciesLockService(SpeciesWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SetSpecieLockForUpdateAsync(
        SpecieId specieId,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Database
            .ExecuteSqlInterpolatedAsync(
                $""" 
                             SELECT 1 FROM species.species
                             WHERE id = {specieId.Value}
                             FOR UPDATE
                 """, cancellationToken);
    }
    
    public async Task SetBreedLockForUpdateAsync(
        BreedId breedId,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Database
            .ExecuteSqlInterpolatedAsync(
                $""" 
                             SELECT 1 FROM species.breeds
                             WHERE id = {breedId.Value}
                             FOR UPDATE
                 """, cancellationToken);
    }
}
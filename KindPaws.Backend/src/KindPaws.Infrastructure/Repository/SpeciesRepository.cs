using KindPaws.Application.Abstractions;
using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Infrastructure.Repository;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly WriteDbContext _dbContext;

    public SpeciesRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Specie specie,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Species.AddAsync(specie, cancellationToken);
    }

    public void Delete(
        Specie specie)
    {
        _dbContext.Species.Remove(specie);
    }

    public async Task<Result<Specie, Error>> GetByIdAsync(
        SpecieId specieId,
        CancellationToken cancellationToken = default)
    {
        var specie = await _dbContext.Species
            .FirstOrDefaultAsync(x => x.Id == specieId, cancellationToken);

        if (specie == null)
            return Errors.General.RecordNotFound(
                nameof(Specie),
                nameof(SpecieId),
                specieId.Value);

        return specie;
    }

    public async Task<Result<Specie, Error>> GetByName(
        ShortName name,
        CancellationToken cancellationToken = default)
    {
        var specie = await _dbContext.Species
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        if (specie == null)
            return Errors.General.RecordNotFound(
                nameof(Specie),
                nameof(SpecieId),
                name.Value);

        return specie;
    }
}
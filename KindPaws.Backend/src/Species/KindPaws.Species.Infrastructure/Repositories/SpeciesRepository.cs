using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Application.Interfaces;
using KindPaws.Species.Domain.AggregateRoot;
using KindPaws.Species.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Species.Infrastructure.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly SpeciesWriteDbContext _dbContext;

    public SpeciesRepository(SpeciesWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Specie specie,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Species.AddAsync(specie, cancellationToken);
    }

    public void SoftDelete(Specie specie)
    {
        _dbContext.Species.Remove(specie);
    }

    public void HardDelete(Specie specie)
    {
        specie.HardDelete();
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

    public async Task<Result<Specie, Error>> GetByNameAsync(
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
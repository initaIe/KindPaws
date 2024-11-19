using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Application.Abstractions;
using KindPaws.Species.Domain.AggregateRoot;
using KindPaws.Species.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Species.Infrastructure.Repositories;

public class SpeciesRepository : IRepository<Specie, SpecieId>
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

    public void Delete(Specie specie)
    {
        _dbContext.Species.Remove(specie);
    }

    public async Task<Result<Specie, Error>> GetByIdAsync(
        SpecieId accountId,
        CancellationToken cancellationToken = default)
    {
        var specie = await _dbContext.Species
            .FirstOrDefaultAsync(x => x.Id == accountId, cancellationToken);

        if (specie == null)
            return Errors.General.RecordNotFound(
                nameof(Specie),
                nameof(SpecieId),
                accountId.Value);

        return specie;
    }
}
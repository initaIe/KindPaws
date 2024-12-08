using KindPaws.Core.Abstractions.DataBase;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
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
        SpecieId permissionId,
        CancellationToken cancellationToken = default)
    {
        var specie = await _dbContext.Species
            .Include(s=>s.Breeds)
            .FirstOrDefaultAsync(x => x.Id == permissionId, cancellationToken);

        if (specie == null)
            return GeneralErrors.General.RecordNotFound(
                nameof(Specie),
                nameof(SpecieId),
                permissionId.Value);

        return specie;
    }
}
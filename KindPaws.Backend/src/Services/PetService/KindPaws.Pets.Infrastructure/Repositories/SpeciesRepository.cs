using KindPaws.Core.Abstractions.Database;
using KindPaws.Pets.Domain.SpeciesManagement.AggregateRoot;
using KindPaws.Pets.Infrastructure.DbContexts;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Pets.Infrastructure.Repositories;

public class SpeciesRepository : IRepository<Specie, SpecieId>
{
    private readonly PetsWriteDbContext _dbContext;

    public SpeciesRepository(PetsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Result<Specie, Error>> GetByIdAsync(
        SpecieId specieId,
        CancellationToken cancellationToken = default)
    {
        var specie = await _dbContext.Species
            .Include(s => s.Breeds)
            .FirstOrDefaultAsync(
                s => s.Id == specieId,
                cancellationToken);

        if (specie == null)
            return ErrorsGeneral.RecordNotFound(
                nameof(Specie),
                nameof(SpecieId),
                specieId.Value);

        return specie;
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
}
using KindPaws.Core.Abstractions.Database;
using KindPaws.Pets.Domain.VolunteersManagement.AggregateRoot;
using KindPaws.Pets.Infrastructure.DbContexts;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Pets.Infrastructure.Repositories;

public class VolunteersRepository : IRepository<Volunteer, VolunteerId>
{
    private readonly PetsWriteDbContext _dbContext;

    public VolunteersRepository(PetsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Volunteer, Error>> GetByIdAsync(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(
                v => v.Id == volunteerId,
                cancellationToken);

        if (volunteer == null)
            return ErrorsGeneral.RecordNotFound(
                nameof(Volunteer),
                nameof(VolunteerId),
                volunteerId.Value);

        return volunteer;
    }

    public async Task AddAsync(
        Volunteer volunteer,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
    }

    public void Delete(Volunteer volunteer)
    {
        _dbContext.Volunteers.Remove(volunteer);
    }
}
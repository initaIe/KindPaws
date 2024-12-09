using KindPaws.Core.Abstractions.Database;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Infrastructure.Repositories;

public class VolunteersRepository : IRepository<Volunteer, VolunteerId>
{
    private readonly VolunteersWriteDbContext _dbContext;

    public VolunteersRepository(VolunteersWriteDbContext dbContext)
    {
        _dbContext = dbContext;
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

    public async Task<Result<Volunteer, Error>> GetByIdAsync(
        VolunteerId permissionId,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v=>v.Pets)
            .FirstOrDefaultAsync(x => x.Id == permissionId, cancellationToken);

        if (volunteer == null)
            return ErrorsGeneral.RecordNotFound(
                nameof(Volunteer),
                nameof(VolunteerId),
                permissionId.Value);

        return volunteer;
    }
}
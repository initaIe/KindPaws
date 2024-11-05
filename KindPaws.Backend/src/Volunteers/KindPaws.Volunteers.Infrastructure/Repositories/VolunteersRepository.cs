using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteersRepository
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
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(x => x.Id == volunteerId, cancellationToken);

        if (volunteer == null)
            return Errors.General.RecordNotFound(
                nameof(Volunteer),
                nameof(VolunteerId),
                volunteerId.Value);

        return volunteer;
    }
}
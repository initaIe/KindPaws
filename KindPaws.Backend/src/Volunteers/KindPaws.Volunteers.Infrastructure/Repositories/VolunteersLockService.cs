using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Abstractions;
using KindPaws.Volunteers.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Infrastructure.Repositories;

public class VolunteersLockService : IVolunteersLockService
{
    private readonly VolunteersWriteDbContext _dbContext;

    public VolunteersLockService(VolunteersWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SetVolunteerLockForUpdateAsync(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Database
            .ExecuteSqlInterpolatedAsync(
                $""" 
                             SELECT 1 FROM volunteers.volunteers
                             WHERE id = {volunteerId.Value}
                             FOR UPDATE
                 """, cancellationToken);
    }

    public async Task SetPetLockForUpdateAsync(
        PetId petId,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Database
            .ExecuteSqlInterpolatedAsync(
                $""" 
                             SELECT 1 FROM volunteers.pets
                             WHERE id = {petId.Value}
                             FOR UPDATE
                 """, cancellationToken);
    }
}
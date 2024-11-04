using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;
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

    public void SoftDelete(Volunteer volunteer)
    {
        _dbContext.Volunteers.Remove(volunteer);
    }

    public void HardDelete(Volunteer volunteer)
    {
        volunteer.HardDelete();
        _dbContext.Volunteers.Remove(volunteer);
    }

    public async Task<Result<Volunteer, Error>> GetByEmailAddressAsync(
        EmailAddress emailAddress,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(x => x.EmailAddress == emailAddress, cancellationToken);

        if (volunteer == null)
            return Errors.General.RecordNotFound(
                nameof(Volunteer),
                nameof(EmailAddress),
                emailAddress.Value);

        return volunteer;
    }

    public async Task<Result<Volunteer, Error>> GetByPhoneNumberAsync(
        PhoneNumber phoneNumber,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(v => v.PhoneNumber == phoneNumber, cancellationToken);

        if (volunteer == null)
            return Errors.General.RecordNotFound(
                nameof(Volunteer),
                nameof(PhoneNumber),
                phoneNumber.Value);

        return volunteer;
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
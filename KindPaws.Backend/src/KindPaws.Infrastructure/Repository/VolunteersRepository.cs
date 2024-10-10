using KindPaws.Application.Volunteers;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Infrastructure.Repository;

public class VolunteersRepository : IVolunteersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VolunteersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync(
        Volunteer volunteer,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
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

    public async Task<Guid> SaveAsync(
        Volunteer volunteer,
        CancellationToken cancellationToken = default)
    {
        var entries = _dbContext.ChangeTracker.Entries<Volunteer>();
        // BUG: Temporarily removed
        // _dbContext.Attach(volunteer);
        var entries1 = _dbContext.ChangeTracker.Entries<Volunteer>();
        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }

    public async Task<Guid> DeleteAsync(
        Volunteer volunteer,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Remove(volunteer);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }
}
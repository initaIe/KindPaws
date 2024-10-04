using KindPaws.Application.Volunteers;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.Others;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Infrastructure.Repository;

// TODO: add includes
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
            return Errors.General.RecordNotFound(nameof(Volunteer), emailAddress.Value);

        return volunteer;
    }

    public async Task<Result<Volunteer, Error>> GetByPhoneNumberAsync(
        PhoneNumber phoneNumber,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);

        if (volunteer == null)
            return Errors.General.RecordNotFound(nameof(Volunteer), phoneNumber.Value);

        return volunteer;
    }

    public async Task<Result<Volunteer, Error>> GetByIdAsync(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(x => x.Pets)
            .FirstOrDefaultAsync(x => x.Id == volunteerId, cancellationToken);

        if (volunteer == null)
            return Errors.General.RecordNotFound(nameof(Volunteer), volunteerId);

        return volunteer;
    }
}
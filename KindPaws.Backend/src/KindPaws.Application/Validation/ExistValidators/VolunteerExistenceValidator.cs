using KindPaws.Application.Abstractions.EntitiesExistValidators;
using KindPaws.Application.Abstractions.IoC;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Application.Validation.ExistValidators;

public class VolunteerExistenceValidator : IVolunteerExistenceValidator
{
    private readonly IReadDbContext _readDbContext;

    public VolunteerExistenceValidator(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<bool> IsVolunteerWithIdExistsAsync(
        Guid volunteerId,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Volunteers.AnyAsync(
            v => v.Id == volunteerId,
            cancellationToken);
    }

    public async Task<bool> IsVolunteerWithEmailAddressExistsAsync(
        string emailAddress,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Volunteers.AnyAsync(
            v => v.EmailAddress.Equals(emailAddress, StringComparison.CurrentCultureIgnoreCase),
            cancellationToken);
    }

    public async Task<bool> IsVolunteerWithPhoneNumberExistsAsync(
        string phoneNumber,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Volunteers.AnyAsync(
            v => v.PhoneNumber.Equals(phoneNumber, StringComparison.CurrentCultureIgnoreCase),
            cancellationToken);
    }
}
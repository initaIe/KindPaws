using KindPaws.Application.Abstractions.ExistValidators;
using KindPaws.Application.Abstractions.IoC;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Application.Validation.ExistValidators;

public class VolunteerExistValidator : IVolunteerExistValidator
{
    private readonly IReadDbContext _readDbContext;

    public VolunteerExistValidator(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<bool> IsVolunteerByIdExistsAsync(
        Guid volunteerId,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Volunteers.AnyAsync(
            v => v.Id == volunteerId,
            cancellationToken);
    }

    public async Task<bool> IsVolunteerByEmailAddressExistsAsync(
        string emailAddress,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Volunteers.AnyAsync(
            v => v.EmailAddress.Equals(emailAddress, StringComparison.CurrentCultureIgnoreCase),
            cancellationToken);
    }

    public async Task<bool> IsVolunteerByPhoneNumberExistsAsync(
        string phoneNumber,
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Volunteers.AnyAsync(
            v => v.PhoneNumber.Equals(phoneNumber, StringComparison.CurrentCultureIgnoreCase),
            cancellationToken);
    }
}
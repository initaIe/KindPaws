namespace KindPaws.Application.Abstractions.ExistValidators;

public interface IVolunteerExistValidator
{
    Task<bool> IsVolunteerByIdExistsAsync(Guid volunteerId, CancellationToken cancellationToken);
    Task<bool> IsVolunteerByEmailAddressExistsAsync(string emailAddress, CancellationToken cancellationToken);
    Task<bool> IsVolunteerByPhoneNumberExistsAsync(string phoneNumber, CancellationToken cancellationToken);
}
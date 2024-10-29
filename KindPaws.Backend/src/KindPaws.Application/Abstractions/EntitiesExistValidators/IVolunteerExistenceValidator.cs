namespace KindPaws.Application.Abstractions.EntitiesExistValidators;

public interface IVolunteerExistenceValidator
{
    Task<bool> IsVolunteerWithIdExistsAsync(Guid volunteerId, CancellationToken cancellationToken);
    Task<bool> IsVolunteerWithEmailAddressExistsAsync(string emailAddress, CancellationToken cancellationToken);
    Task<bool> IsVolunteerWithPhoneNumberExistsAsync(string phoneNumber, CancellationToken cancellationToken);
}
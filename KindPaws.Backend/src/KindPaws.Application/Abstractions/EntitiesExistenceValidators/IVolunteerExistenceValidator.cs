namespace KindPaws.Application.Abstractions.EntitiesExistenceValidators;

public interface IVolunteerExistenceValidator
{
    Task<bool> IsVolunteerByIdExistsAsync(
        Guid volunteerId,
        CancellationToken cancellationToken);

    Task<bool> IsVolunteerByEmailAddressExistsAsync(
        string emailAddress,
        CancellationToken cancellationToken);

    Task<bool> IsVolunteerByPhoneNumberExistsAsync(
        string phoneNumber,
        CancellationToken cancellationToken);
}
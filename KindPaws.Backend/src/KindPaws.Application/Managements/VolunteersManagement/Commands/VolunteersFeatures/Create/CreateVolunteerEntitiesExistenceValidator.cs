using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistenceValidators;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;

public class
    CreateVolunteerEntitiesExistenceValidator : IEntitiesExistenceValidator<CreateVolunteerExistenceValidationData>
{
    private readonly IVolunteerExistenceValidator _volunteerExistenceValidator;

    public CreateVolunteerEntitiesExistenceValidator(IVolunteerExistenceValidator volunteerExistenceValidator)
    {
        _volunteerExistenceValidator = volunteerExistenceValidator;
    }

    public async Task<Result<Error>> ValidateAsync(CreateVolunteerExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByEmailAddressExist = await _volunteerExistenceValidator
            .IsVolunteerByEmailAddressExistsAsync(validationData.EmailAddress, cancellationToken);
        if (isVolunteerByEmailAddressExist)
            return Errors.General.RecordAlreadyExist(nameof(Volunteer), nameof(EmailAddress));

        var isVolunteerByPhoneNumberExist = await _volunteerExistenceValidator
            .IsVolunteerByPhoneNumberExistsAsync(validationData.PhoneNumber, cancellationToken);
        if (isVolunteerByPhoneNumberExist)
            return Errors.General.RecordAlreadyExist(nameof(Volunteer), nameof(PhoneNumber));

        return true;
    }
}
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.ExistValidators;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;

public class CreateVolunteerEntitiesExistenceChecker : IEntitiesExistenceChecker<CreateVolunteerExistenceCheckData>
{
    private readonly IVolunteerExistValidator _volunteerExistValidator;

    public CreateVolunteerEntitiesExistenceChecker(IVolunteerExistValidator volunteerExistValidator)
    {
        _volunteerExistValidator = volunteerExistValidator;
    }

    public async Task<Result<Error>> CheckAsync(CreateVolunteerExistenceCheckData checkData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByEmailAddressExist = await _volunteerExistValidator
            .IsVolunteerByEmailAddressExistsAsync(checkData.EmailAddress, cancellationToken);
        if (isVolunteerByEmailAddressExist)
            return Errors.General.RecordAlreadyExist(nameof(Volunteer), nameof(EmailAddress));

        var isVolunteerByPhoneNumberExist = await _volunteerExistValidator
            .IsVolunteerByPhoneNumberExistsAsync(checkData.PhoneNumber, cancellationToken);
        if (isVolunteerByPhoneNumberExist)
            return Errors.General.RecordAlreadyExist(nameof(Volunteer), nameof(PhoneNumber));

        return true;
    }
}
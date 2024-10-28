using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.ExistValidators;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateMainInfo;

public class UpdateVolunteerMainInfoEntitiesExistenceChecker 
    : IEntitiesExistenceChecker<UpdateVolunteerMainInfoExistenceCheckData>
{
    private readonly IVolunteerExistValidator _volunteerExistValidator;

    public UpdateVolunteerMainInfoEntitiesExistenceChecker(IVolunteerExistValidator volunteerExistValidator)
    {
        _volunteerExistValidator = volunteerExistValidator;
    }

    public async Task<Result<Error>> CheckAsync(
        UpdateVolunteerMainInfoExistenceCheckData checkData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _volunteerExistValidator
            .IsVolunteerByIdExistsAsync(checkData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), checkData.VolunteerId);
        
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
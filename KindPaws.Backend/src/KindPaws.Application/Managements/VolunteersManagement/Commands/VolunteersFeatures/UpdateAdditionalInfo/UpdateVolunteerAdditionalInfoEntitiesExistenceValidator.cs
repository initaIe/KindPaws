using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistValidators;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateAdditionalInfo;

public class UpdateVolunteerAdditionalInfoEntitiesExistenceValidator
    : IEntitiesExistenceValidator<UpdateVolunteerAdditionalInfoExistenceValidationData>
{
    private readonly IVolunteerExistenceValidator _volunteerExistenceValidator;

    public UpdateVolunteerAdditionalInfoEntitiesExistenceValidator(IVolunteerExistenceValidator volunteerExistenceValidator)
    {
        _volunteerExistenceValidator = volunteerExistenceValidator;
    }

    public async Task<Result<Error>> ValidateAsync(
        UpdateVolunteerAdditionalInfoExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _volunteerExistenceValidator
            .IsVolunteerWithIdExistsAsync(validationData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), validationData.VolunteerId);

        return true;
    }
}
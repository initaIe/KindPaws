using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistenceValidators;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.HardDelete;

public class HardDeleteVolunteerEntitiesExistenceValidator
    : IEntitiesExistenceValidator<HardDeleteVolunteerExistenceValidationData>
{
    private readonly IVolunteerExistenceValidator _volunteerExistenceValidator;

    public HardDeleteVolunteerEntitiesExistenceValidator(IVolunteerExistenceValidator volunteerExistenceValidator)
    {
        _volunteerExistenceValidator = volunteerExistenceValidator;
    }

    public async Task<Result<Error>> ValidateAsync(
        HardDeleteVolunteerExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _volunteerExistenceValidator
            .IsVolunteerByIdExistsAsync(validationData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), validationData.VolunteerId);

        return true;
    }
}
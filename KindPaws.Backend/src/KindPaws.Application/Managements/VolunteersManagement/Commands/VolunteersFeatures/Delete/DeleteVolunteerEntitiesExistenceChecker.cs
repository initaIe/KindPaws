using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.ExistValidators;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Delete;

public class DeleteVolunteerEntitiesExistenceChecker : IEntitiesExistenceChecker<DeleteVolunteerExistenceCheckData>
{
    private readonly IVolunteerExistValidator _volunteerExistValidator;

    public DeleteVolunteerEntitiesExistenceChecker(IVolunteerExistValidator volunteerExistValidator)
    {
        _volunteerExistValidator = volunteerExistValidator;
    }

    public async Task<Result<Error>> CheckAsync(DeleteVolunteerExistenceCheckData checkData, CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _volunteerExistValidator
            .IsVolunteerByIdExistsAsync(checkData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), checkData.VolunteerId);

        return true;
    }
}
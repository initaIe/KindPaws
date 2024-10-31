using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistenceValidators;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdatePosition;

public class UpdatePetPositionEntitiesExistenceValidator
    : IEntitiesExistenceValidator<UpdatePetPositionExistenceValidationData>
{
    private readonly IPetExistenceValidator _petExistenceValidator;
    private readonly IVolunteerExistenceValidator _volunteerExistenceValidator;

    public UpdatePetPositionEntitiesExistenceValidator(
        IPetExistenceValidator petExistenceValidator,
        IVolunteerExistenceValidator volunteerExistenceValidator)
    {
        _petExistenceValidator = petExistenceValidator;
        _volunteerExistenceValidator = volunteerExistenceValidator;
    }

    public async Task<Result<Error>> ValidateAsync(
        UpdatePetPositionExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _volunteerExistenceValidator
            .IsVolunteerByIdExistsAsync(validationData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), validationData.VolunteerId);

        var isPetWithIdExistForVolunteerWithId = await _petExistenceValidator
            .IsPetByIdForVolunteerByIdExistsAsync(validationData.VolunteerId, validationData.PetId,
                cancellationToken);
        if (!isPetWithIdExistForVolunteerWithId)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), validationData.PetId);

        return true;
    }
}
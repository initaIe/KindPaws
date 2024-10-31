using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistenceValidators;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.SoftDelete;

public class
    SoftDeletePetEntitiesExistenceValidator : IEntitiesExistenceValidator<SoftDeletePetExistenceValidationData>
{
    private readonly IVolunteerExistenceValidator _volunteerExistenceValidator;
    private readonly IPetExistenceValidator _petExistenceValidator;

    public SoftDeletePetEntitiesExistenceValidator(
        IVolunteerExistenceValidator volunteerExistenceValidator,
        IPetExistenceValidator petExistenceValidator)
    {
        _volunteerExistenceValidator = volunteerExistenceValidator;
        _petExistenceValidator = petExistenceValidator;
    }

    public async Task<Result<Error>> ValidateAsync(
        SoftDeletePetExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _volunteerExistenceValidator
            .IsVolunteerByIdExistsAsync(validationData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), validationData.VolunteerId);

        var isPetByIdExist = await _petExistenceValidator
            .IsPetByIdForVolunteerByIdExistsAsync(validationData.VolunteerId, validationData.PetId, cancellationToken);
        if (!isPetByIdExist)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), validationData.PetId);

        return true;
    }
}
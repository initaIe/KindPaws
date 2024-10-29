using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistValidators;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.AddPhotos;

public class AddPetPhotosEntitiesExistenceValidator : IEntitiesExistenceValidator<AddPetPhotosExistenceValidationData>
{
    private readonly IPetExistenceValidator _petExistenceValidator;
    private readonly IVolunteerExistenceValidator _volunteerExistenceValidator;

    public AddPetPhotosEntitiesExistenceValidator(
        IVolunteerExistenceValidator volunteerExistenceValidator,
        IPetExistenceValidator petExistenceValidator)
    {
        _volunteerExistenceValidator = volunteerExistenceValidator;
        _petExistenceValidator = petExistenceValidator;
    }

    public async Task<Result<Error>> ValidateAsync(
        AddPetPhotosExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _volunteerExistenceValidator
            .IsVolunteerWithIdExistsAsync(validationData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), validationData.VolunteerId);

        var isPetWithIdExistForVolunteerWithId = await _petExistenceValidator
            .IsPetWithIdExistsForVolunteerWithIdAsync(validationData.VolunteerId, validationData.PetId, cancellationToken);
        if (!isPetWithIdExistForVolunteerWithId)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), validationData.PetId);

        return true;
    }
}
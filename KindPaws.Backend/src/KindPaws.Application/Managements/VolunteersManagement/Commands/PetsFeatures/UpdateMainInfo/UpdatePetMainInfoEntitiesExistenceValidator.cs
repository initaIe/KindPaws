using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistValidators;
using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateMainInfo;

public class UpdatePetMainInfoEntitiesExistenceValidator
    : IEntitiesExistenceValidator<UpdatePetMainInfoExistenceValidationData>
{
    private readonly IBreedExistenceValidator _breedExistenceValidator;
    private readonly IPetExistenceValidator _petExistenceValidator;
    private readonly ISpecieExistenceValidator _specieExistenceValidator;
    private readonly IVolunteerExistenceValidator _volunteerExistenceValidator;

    public UpdatePetMainInfoEntitiesExistenceValidator(
        IVolunteerExistenceValidator volunteerExistenceValidator,
        IPetExistenceValidator petExistenceValidator,
        ISpecieExistenceValidator specieExistenceValidator,
        IBreedExistenceValidator breedExistenceValidator)
    {
        _volunteerExistenceValidator = volunteerExistenceValidator;
        _petExistenceValidator = petExistenceValidator;
        _specieExistenceValidator = specieExistenceValidator;
        _breedExistenceValidator = breedExistenceValidator;
    }

    public async Task<Result<Error>> ValidateAsync(
        UpdatePetMainInfoExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _volunteerExistenceValidator
            .IsVolunteerWithIdExistsAsync(validationData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), validationData.VolunteerId);

        var isPetWithIdExistForVolunteerWithId = await _petExistenceValidator
            .IsPetWithIdExistsForVolunteerWithIdAsync(validationData.VolunteerId, validationData.PetId,
                cancellationToken);
        if (!isPetWithIdExistForVolunteerWithId)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), validationData.PetId);

        var isSpecieByIdExist = await _specieExistenceValidator
            .IsSpecieWithIdExistsAsync(validationData.SpecieId, cancellationToken);
        if (!isSpecieByIdExist)
            return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), validationData.SpecieId);

        var isBreedWithIdExistForSpecieWithId = await _breedExistenceValidator
            .IsBreedWithIdExistsForSpecieWithIdAsync(validationData.SpecieId, validationData.BreedId,
                cancellationToken);
        if (!isBreedWithIdExistForSpecieWithId)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(BreedId), validationData.BreedId);

        return true;
    }
}
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistValidators;
using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.Add;

public class AddPetEntitiesExistenceValidator : IEntitiesExistenceValidator<AddPetExistenceValidationData>
{
    private readonly IBreedExistenceValidator _breedExistenceValidator;
    private readonly ISpecieExistenceValidator _specieExistenceValidator;
    private readonly IVolunteerExistenceValidator _volunteerExistenceValidator;

    public AddPetEntitiesExistenceValidator(
        IVolunteerExistenceValidator volunteerExistenceValidator,
        ISpecieExistenceValidator specieExistenceValidator,
        IBreedExistenceValidator breedExistenceValidator)
    {
        _volunteerExistenceValidator = volunteerExistenceValidator;
        _specieExistenceValidator = specieExistenceValidator;
        _breedExistenceValidator = breedExistenceValidator;
    }

    public async Task<Result<Error>> ValidateAsync(
        AddPetExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _volunteerExistenceValidator
            .IsVolunteerWithIdExistsAsync(validationData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), validationData.VolunteerId);

        var isSpecieByIdExist = await _specieExistenceValidator
            .IsSpecieWithIdExistsAsync(validationData.SpecieId, cancellationToken);
        if (!isSpecieByIdExist)
            return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), validationData.SpecieId);

        var isBreedByIdExistForSpecieWithId = await _breedExistenceValidator
            .IsBreedWithIdExistsForSpecieWithIdAsync(validationData.SpecieId, validationData.BreedId,
                cancellationToken);
        if (!isBreedByIdExistForSpecieWithId)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(BreedId), validationData.BreedId);

        return true;
    }
}
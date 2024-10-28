using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.ExistValidators;
using KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Create;
using KindPaws.Application.Validation.ExistValidators;
using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.Add;

public class AddPetEntitiesExistenceChecker : IEntitiesExistenceChecker<AddPetExistenceCheckData>
{
    private readonly IVolunteerExistValidator _volunteerExistValidator;
    private readonly ISpecieExistValidator _specieExistValidator;
    private readonly IBreedExistValidator _breedExistValidator;

    public AddPetEntitiesExistenceChecker(
        IVolunteerExistValidator volunteerExistValidator,
        ISpecieExistValidator specieExistValidator,
        IBreedExistValidator breedExistValidator)
    {
        _volunteerExistValidator = volunteerExistValidator;
        _specieExistValidator = specieExistValidator;
        _breedExistValidator = breedExistValidator;
    }

    public async Task<Result<Error>> CheckAsync(
        AddPetExistenceCheckData checkData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _volunteerExistValidator
            .IsVolunteerByIdExistsAsync(checkData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), checkData.VolunteerId);

        var isSpecieByIdExist = await _specieExistValidator
            .IsSpecieByIdExistsAsync(checkData.SpecieId, cancellationToken);
        if (!isSpecieByIdExist)
            return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), checkData.SpecieId);

        var isBreedByIdExist = await _breedExistValidator
            .IsBreedByIdExistsAsync(checkData.BreedId, cancellationToken);
        if (!isBreedByIdExist)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(BreedId), checkData.BreedId);

        return true;
    }
}
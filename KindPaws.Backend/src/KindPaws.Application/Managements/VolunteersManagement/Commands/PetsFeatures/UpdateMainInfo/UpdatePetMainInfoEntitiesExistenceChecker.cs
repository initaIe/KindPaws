using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.ExistValidators;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateAdditionalInfo;
using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateMainInfo;

public class UpdatePetMainInfoEntitiesExistenceChecker 
    : IEntitiesExistenceChecker<UpdatePetMainInfoExistenceCheckData>
{
    private readonly IVolunteerExistValidator _volunteerExistValidator;
    private readonly IPetExistValidator _petExistValidator;
    private readonly ISpecieExistValidator _specieExistValidator;
    private readonly IBreedExistValidator _breedExistValidator;

    public UpdatePetMainInfoEntitiesExistenceChecker(
        IVolunteerExistValidator volunteerExistValidator,
        IPetExistValidator petExistValidator, 
        ISpecieExistValidator specieExistValidator,
        IBreedExistValidator breedExistValidator)
    {
        _volunteerExistValidator = volunteerExistValidator;
        _petExistValidator = petExistValidator;
        _specieExistValidator = specieExistValidator;
        _breedExistValidator = breedExistValidator;
    }

    public async Task<Result<Error>> CheckAsync(
        UpdatePetMainInfoExistenceCheckData checkData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _volunteerExistValidator
            .IsVolunteerByIdExistsAsync(checkData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), checkData.VolunteerId);
        
        var isPetByIdExist = await _petExistValidator
            .IsPetByIdExists(checkData.PetId, cancellationToken);
        if (!isPetByIdExist)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), checkData.PetId);
        
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
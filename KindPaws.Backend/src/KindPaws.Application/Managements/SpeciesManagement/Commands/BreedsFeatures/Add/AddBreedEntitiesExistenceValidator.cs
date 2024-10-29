using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistValidators;
using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;

public class AddBreedEntitiesExistenceValidator : IEntitiesExistenceValidator<AddBreedExistenceValidationData>
{
    private readonly IBreedExistenceValidator _breedExistenceValidator;
    private readonly ISpecieExistenceValidator _specieExistenceValidator;

    public AddBreedEntitiesExistenceValidator(
        ISpecieExistenceValidator specieExistenceValidator,
        IBreedExistenceValidator breedExistenceValidator)
    {
        _specieExistenceValidator = specieExistenceValidator;
        _breedExistenceValidator = breedExistenceValidator;
    }

    public async Task<Result<Error>> ValidateAsync(
        AddBreedExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isSpecieWithIdExist = await _specieExistenceValidator
            .IsSpecieWithIdExistsAsync(validationData.SpeciesId, cancellationToken);
        if (!isSpecieWithIdExist)
            return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), validationData.SpeciesId);

        var isBreedWithNameExistsForSpecieWithId = await _breedExistenceValidator
            .IsBreedWithNameExistsForSpecieWithIdAsync(validationData.SpeciesId, validationData.BreedName, cancellationToken);
        if (isBreedWithNameExistsForSpecieWithId)
            return Errors.General.RecordAlreadyExist(nameof(Breed), nameof(ShortName));

        return true;
    }
}
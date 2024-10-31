using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistenceValidators;
using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.SoftDelete;

public class SoftDeleteBreedEntitiesExistenceValidator
    : IEntitiesExistenceValidator<SoftDeleteBreedExistenceValidationData>
{
    private readonly IBreedExistenceValidator _breedExistenceValidator;
    private readonly ISpecieExistenceValidator _specieExistenceValidator;
    private readonly IPetExistenceValidator _petExistenceValidator;

    public SoftDeleteBreedEntitiesExistenceValidator(
        IBreedExistenceValidator breedExistenceValidator,
        ISpecieExistenceValidator specieExistenceValidator,
        IPetExistenceValidator petExistenceValidator)
    {
        _breedExistenceValidator = breedExistenceValidator;
        _specieExistenceValidator = specieExistenceValidator;
        _petExistenceValidator = petExistenceValidator;
    }

    public async Task<Result<Error>> ValidateAsync(
        SoftDeleteBreedExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isSpecieWithIdExist = await _specieExistenceValidator
            .IsSpecieByIdExistsAsync(validationData.SpecieId, cancellationToken);
        if (!isSpecieWithIdExist)
            return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), validationData.SpecieId);

        var isBreedWithIdExistForSpecieWithId = await _breedExistenceValidator
            .IsBreedByIdForSpecieByIdExistsAsync(validationData.SpecieId, validationData.BreedId, cancellationToken);
        if (!isBreedWithIdExistForSpecieWithId)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(BreedId), validationData.BreedId);

        var isPetWithBreedIdExist = await _petExistenceValidator
            .IsPetByBreedIdExistsAsync(validationData.BreedId, cancellationToken);
        if (isPetWithBreedIdExist)
            return Errors.General.OperationCanNotBePerformed(
                "Delete breed",
                "because exists pet with this breed");

        return true;
    }
}
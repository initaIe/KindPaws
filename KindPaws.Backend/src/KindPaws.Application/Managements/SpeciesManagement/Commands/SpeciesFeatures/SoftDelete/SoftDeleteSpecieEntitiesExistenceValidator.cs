using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistenceValidators;
using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.SoftDelete;

public class SoftDeleteSpecieEntitiesExistenceValidator
    : IEntitiesExistenceValidator<SoftDeleteSpecieExistenceValidationData>
{
    private readonly IBreedExistenceValidator _breedExistenceValidator;
    private readonly ISpecieExistenceValidator _specieExistenceValidator;
    private readonly IPetExistenceValidator _petExistenceValidator;

    public SoftDeleteSpecieEntitiesExistenceValidator(
        IBreedExistenceValidator breedExistenceValidator,
        ISpecieExistenceValidator specieExistenceValidator,
        IPetExistenceValidator petExistenceValidator)
    {
        _breedExistenceValidator = breedExistenceValidator;
        _specieExistenceValidator = specieExistenceValidator;
        _petExistenceValidator = petExistenceValidator;
    }

    public async Task<Result<Error>> ValidateAsync(
        SoftDeleteSpecieExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isSpecieByIdExist = await _specieExistenceValidator
            .IsSpecieByIdExistsAsync(validationData.SpecieId, cancellationToken);
        if (!isSpecieByIdExist)
            return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), validationData.SpecieId);

        var isPetBySpecieIdExist = await _petExistenceValidator
            .IsPetBySpecieIdExistsAsync(validationData.SpecieId, cancellationToken);
        if (isPetBySpecieIdExist)
            return Errors.General.OperationCanNotBePerformed(
                "Delete specie",
                "because exists pet with this specie");

        return true;
    }
}
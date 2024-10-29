using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistValidators;
using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Create;

public class CreateSpecieEntitiesExistenceValidator : IEntitiesExistenceValidator<CreateSpecieExistenceValidationData>
{
    private readonly ISpecieExistenceValidator _specieExistenceValidator;

    public CreateSpecieEntitiesExistenceValidator(ISpecieExistenceValidator specieExistenceValidator)
    {
        _specieExistenceValidator = specieExistenceValidator;
    }

    public async Task<Result<Error>> ValidateAsync(
        CreateSpecieExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isSpecieByNameExist = await _specieExistenceValidator
            .IsSpecieWithNameExistsAsync(validationData.Name, cancellationToken);
        if (isSpecieByNameExist)
            return Errors.General.RecordAlreadyExist(nameof(Specie), nameof(ShortName));

        return true;
    }
}
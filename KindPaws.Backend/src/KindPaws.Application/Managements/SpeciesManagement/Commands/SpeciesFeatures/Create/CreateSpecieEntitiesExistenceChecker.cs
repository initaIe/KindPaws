using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.ExistValidators;
using KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;
using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Create;

public class CreateSpecieEntitiesExistenceChecker : IEntitiesExistenceChecker<CreateSpecieExistenceCheckData>
{
    private readonly ISpecieExistValidator _specieExistValidator;

    public CreateSpecieEntitiesExistenceChecker(ISpecieExistValidator specieExistValidator)
    {
        _specieExistValidator = specieExistValidator;
    }

    public async Task<Result<Error>> CheckAsync(
        CreateSpecieExistenceCheckData checkData,
        CancellationToken cancellationToken)
    {
        var isSpecieByNameExist = await _specieExistValidator
            .IsSpecieByNameExistsAsync(checkData.Name, cancellationToken);
        if (isSpecieByNameExist)
            return Errors.General.RecordAlreadyExist(nameof(Specie), nameof(ShortName));

        return true;
    }
}
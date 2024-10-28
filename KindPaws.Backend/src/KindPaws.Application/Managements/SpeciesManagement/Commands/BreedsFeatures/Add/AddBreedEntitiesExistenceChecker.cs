using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.ExistValidators;
using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;

public class AddBreedEntitiesExistenceChecker : IEntitiesExistenceChecker<AddBreedExistenceCheckData>
{
    private readonly ISpecieExistValidator _specieExistValidator;
    private readonly IBreedExistValidator _breedExistValidator;


    public AddBreedEntitiesExistenceChecker(
        ISpecieExistValidator specieExistValidator,
        IBreedExistValidator breedExistValidator)
    {
        _specieExistValidator = specieExistValidator;
        _breedExistValidator = breedExistValidator;
    }

    public async Task<Result<Error>> CheckAsync(
        AddBreedExistenceCheckData checkData,
        CancellationToken cancellationToken)
    {
        var isSpecieByIdExist = await _specieExistValidator
            .IsSpecieByIdExistsAsync(checkData.SpeciesId, cancellationToken);
        if (!isSpecieByIdExist)
            return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), checkData.SpeciesId);

        var isBreedNyNameExist = await _breedExistValidator
            .IsBreedByNameExistsAsync(checkData.BreedName, cancellationToken);
        if (isBreedNyNameExist)
            return Errors.General.RecordAlreadyExist(nameof(Breed), nameof(ShortName));

        return true;
    }
}
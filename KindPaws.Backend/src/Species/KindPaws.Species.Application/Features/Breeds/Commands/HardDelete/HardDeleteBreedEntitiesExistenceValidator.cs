using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Application.Interfaces;
using KindPaws.Species.Domain.AggregateRoot;
using KindPaws.Species.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Species.Application.Features.Breeds.Commands.HardDelete;

public class HardDeleteBreedEntitiesExistenceValidator
    : IEntitiesExistenceValidator<HardDeleteBreedExistenceValidationData>
{
    private readonly ISpeciesReadDbContext _readDbContext;

    public HardDeleteBreedEntitiesExistenceValidator(ISpeciesReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<Error>> ValidateAsync(
        HardDeleteBreedExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isSpecieByIdExist = await _readDbContext.Species.AnyAsync(
            s => s.Id == validationData.SpecieId, cancellationToken);
        if (!isSpecieByIdExist)
            return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), validationData.SpecieId);

        var isBreedWithIdExistForSpecieWithId = await _readDbContext.Breeds.AnyAsync(
            b => b.SpecieId == validationData.SpecieId && b.Id == validationData.BreedId, cancellationToken);
        if (!isBreedWithIdExistForSpecieWithId)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(BreedId), validationData.BreedId);

        // var isPetWithBreedIdExist = await _readDbContext.Pets.AnyAsync(
        //     p => p.BreedId == breedId, cancellationToken);
        // if (isPetWithBreedIdExist)
        //     return Errors.General.OperationCanNotBePerformed(
        //         "Delete breed",
        //         "because exists pet with this breed");

        return true;
    }
}
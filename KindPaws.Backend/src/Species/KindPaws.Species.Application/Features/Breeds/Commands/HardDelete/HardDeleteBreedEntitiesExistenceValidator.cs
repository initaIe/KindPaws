using KindPaws.Core.Abstractions.Validators;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Application.Interfaces;
using KindPaws.Species.Domain.AggregateRoot;
using KindPaws.Species.Domain.Entities;
using KindPaws.Volunteers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Species.Application.Features.Breeds.Commands.HardDelete;

public class HardDeleteBreedEntitiesExistenceValidator
    : IEntitiesExistenceValidator<HardDeleteBreedExistenceValidationData>
{
    private readonly ISpeciesReadDbContext _readDbContext;
    private readonly IVolunteersContract _volunteersContract;

    public HardDeleteBreedEntitiesExistenceValidator(
        ISpeciesReadDbContext readDbContext,
        IVolunteersContract volunteersContract)
    {
        _readDbContext = readDbContext;
        _volunteersContract = volunteersContract;
    }

    public async Task<Result<Error>> ValidateAsync(
        HardDeleteBreedExistenceValidationData validationData,
        CancellationToken cancellationToken = default)
    {
        var isSpecieByIdExist = await _readDbContext.Species.AnyAsync(
            s => s.Id == validationData.SpecieId, cancellationToken);
        if (!isSpecieByIdExist)
            return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), validationData.SpecieId);

        var isBreedWithIdExistForSpecieWithId = await _readDbContext.Breeds.AnyAsync(
            b => b.SpecieId == validationData.SpecieId && b.Id == validationData.BreedId, cancellationToken);
        if (!isBreedWithIdExistForSpecieWithId)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(BreedId), validationData.BreedId);

        var isPetByBreedIdExist = await _volunteersContract
            .IsPetByBreedIdExistsAsync(validationData.BreedId, cancellationToken);
        if (isPetByBreedIdExist)
            return Errors.General.OperationCanNotBePerformed(
                "Hard delete breed",
                "because exist pet with this breed");

        return true;
    }
}
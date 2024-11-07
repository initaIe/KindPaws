using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Application.Interfaces;
using KindPaws.Species.Domain.AggregateRoot;
using KindPaws.Species.Domain.Entities;
using KindPaws.Volunteers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Species.Application.Features.Breeds.Commands.SoftDelete;

public class SoftDeleteBreedEntitiesExistenceValidator
    : IEntitiesExistenceValidator<SoftDeleteBreedExistenceValidationData>
{
    private readonly ISpeciesReadDbContext _readDbContext;
    private readonly IVolunteersContract _volunteersContract;

    public SoftDeleteBreedEntitiesExistenceValidator(
        ISpeciesReadDbContext readDbContext,
        IVolunteersContract volunteersContract)
    {
        _readDbContext = readDbContext;
        _volunteersContract = volunteersContract;
    }

    public async Task<Result<Error>> ValidateAsync(
        SoftDeleteBreedExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isSpecieByIdExist = await _readDbContext.Species.AnyAsync(
            s => s.Id == validationData.SpecieId, cancellationToken);
        if (!isSpecieByIdExist)
            return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), validationData.SpecieId);

        var isBreedByIdForSpecieByIdExist = await _readDbContext.Breeds.AnyAsync(
            b => b.SpecieId == validationData.SpecieId && b.Id == validationData.BreedId, cancellationToken);
        if (!isBreedByIdForSpecieByIdExist)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(BreedId), validationData.BreedId);

        var isPetByBreedIdExist = await _volunteersContract
            .IsPetByBreedIdExistsAsync(validationData.BreedId, cancellationToken);
        if (isPetByBreedIdExist)
            return Errors.General.OperationCanNotBePerformed(
                "Soft delete breed",
                "because exist pet with this breed");

        return true;
    }
}
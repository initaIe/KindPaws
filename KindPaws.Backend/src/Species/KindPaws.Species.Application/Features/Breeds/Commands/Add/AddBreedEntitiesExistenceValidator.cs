using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Application.Interfaces;
using KindPaws.Species.Domain.AggregateRoot;
using KindPaws.Species.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Species.Application.Features.Breeds.Commands.Add;

public class AddBreedEntitiesExistenceValidator : IEntitiesExistenceValidator<AddBreedExistenceValidationData>
{
    private readonly ISpeciesReadDbContext _readDbContext;

    public AddBreedEntitiesExistenceValidator(ISpeciesReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<Error>> ValidateAsync(
        AddBreedExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isSpecieByIdExist = await _readDbContext.Species.AnyAsync(
            s => s.Id == validationData.SpeciesId, cancellationToken);
        if (!isSpecieByIdExist)
            return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), validationData.SpeciesId);

        var isBreedByNameForSpecieByIdExist = await _readDbContext.Breeds.AnyAsync(
            b => b.SpecieId == validationData.SpeciesId && b.Name == validationData.BreedName,
            cancellationToken);
        if (isBreedByNameForSpecieByIdExist)
            return Errors.General.RecordAlreadyExist(nameof(Breed), nameof(ShortName));

        return true;
    }
}
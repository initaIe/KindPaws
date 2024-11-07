using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.Species.Application.Interfaces;
using KindPaws.Species.Domain.AggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Species.Application.Features.Species.Commands.Create;

public class CreateSpecieEntitiesExistenceValidator : IEntitiesExistenceValidator<CreateSpecieExistenceValidationData>
{
    private readonly ISpeciesReadDbContext _readDbContext;

    public CreateSpecieEntitiesExistenceValidator(ISpeciesReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<Error>> ValidateAsync(
        CreateSpecieExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isSpecieByNameExist = await _readDbContext.Species.AnyAsync(
            s => s.Name == validationData.Name, cancellationToken);
        if (isSpecieByNameExist)
            return Errors.General.RecordAlreadyExist(nameof(Specie), nameof(ShortName));

        return true;
    }
}
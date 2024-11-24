using KindPaws.Core.Abstractions.Validators;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.Species.Application.Abstractions;
using KindPaws.Species.Domain.AggregateRoot;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Species.Application.Features.Species.Commands.CreateSpecie;

public class CreateSpecieEntitiesExistenceValidator : IEntitiesExistenceValidator<CreateSpecieExistenceValidationData>
{
    private readonly ISpeciesReadDbContext _readDbContext;

    public CreateSpecieEntitiesExistenceValidator(ISpeciesReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<Error>> ValidateAsync(
        CreateSpecieExistenceValidationData validationData,
        CancellationToken cancellationToken = default)
    {
        var isSpecieByNameExist = await _readDbContext.Species.AnyAsync(
            s => s.Name == validationData.Name, cancellationToken);
        if (isSpecieByNameExist)
            return Errors.General.RecordAlreadyExist(nameof(Specie), nameof(SpecieName));

        return true;
    }
}
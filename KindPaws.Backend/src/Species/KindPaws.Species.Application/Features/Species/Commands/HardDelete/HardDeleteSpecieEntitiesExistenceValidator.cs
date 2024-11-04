using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Application.Interfaces;
using KindPaws.Species.Domain.AggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Species.Application.Features.Species.Commands.HardDelete;

public class HardDeleteSpecieEntitiesExistenceValidator
    : IEntitiesExistenceValidator<HardDeleteSpecieExistenceValidationData>
{
    private readonly ISpeciesReadDbContext _readDbContext;

    public HardDeleteSpecieEntitiesExistenceValidator(ISpeciesReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<Error>> ValidateAsync(
        HardDeleteSpecieExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isSpecieByIdExist = await _readDbContext.Species.AnyAsync(
            s => s.Id == validationData.SpecieId, cancellationToken);
        if (!isSpecieByIdExist)
            return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), validationData.SpecieId);

        // var isPetBySpecieIdExist = await _petExistenceValidator
        //     .IsPetBySpecieIdExistsAsync(validationData.SpecieId, cancellationToken);
        // if (isPetBySpecieIdExist)
        //     return Errors.General.OperationCanNotBePerformed(
        //         "Delete specie",
        //         "because exists pet with this specie");

        return true;
    }
}
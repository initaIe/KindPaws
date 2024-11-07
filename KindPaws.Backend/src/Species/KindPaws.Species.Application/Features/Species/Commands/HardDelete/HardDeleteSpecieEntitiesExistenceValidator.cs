using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Application.Interfaces;
using KindPaws.Species.Domain.AggregateRoot;
using KindPaws.Volunteers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Species.Application.Features.Species.Commands.HardDelete;

public class HardDeleteSpecieEntitiesExistenceValidator
    : IEntitiesExistenceValidator<HardDeleteSpecieExistenceValidationData>
{
    private readonly ISpeciesReadDbContext _readDbContext;
    private readonly IVolunteersContract _volunteersContract;

    public HardDeleteSpecieEntitiesExistenceValidator(
        ISpeciesReadDbContext readDbContext,
        IVolunteersContract volunteersContract)
    {
        _readDbContext = readDbContext;
        _volunteersContract = volunteersContract;
    }

    public async Task<Result<Error>> ValidateAsync(
        HardDeleteSpecieExistenceValidationData validationData,
        CancellationToken cancellationToken = default)
    {
        var isSpecieByIdExist = await _readDbContext.Species.AnyAsync(
            s => s.Id == validationData.SpecieId, cancellationToken);
        if (!isSpecieByIdExist)
            return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), validationData.SpecieId);

        var isPetBySpecieIdExist = await _volunteersContract
            .IsPetBySpecieIdExistsAsync(validationData.SpecieId, cancellationToken);
        if (isPetBySpecieIdExist)
            return Errors.General.OperationCanNotBePerformed(
                "Hard delete specie",
                "because exist pet with this specie");

        return true;
    }
}
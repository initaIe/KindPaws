using KindPaws.Core.Abstractions.Validators;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Application.Interfaces;
using KindPaws.Species.Domain.AggregateRoot;
using KindPaws.Volunteers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Species.Application.Features.Species.Commands.SoftDelete;

public class SoftDeleteSpecieEntitiesExistenceValidator
    : IEntitiesExistenceValidator<SoftDeleteSpecieExistenceValidationData>
{
    private readonly ISpeciesReadDbContext _readDbContext;
    private readonly IVolunteersContract _volunteersContract;

    public SoftDeleteSpecieEntitiesExistenceValidator(
        ISpeciesReadDbContext readDbContext,
        IVolunteersContract volunteersContract)
    {
        _readDbContext = readDbContext;
        _volunteersContract = volunteersContract;
    }

    public async Task<Result<Error>> ValidateAsync(
        SoftDeleteSpecieExistenceValidationData validationData,
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
                "Soft delete specie",
                "because exist pet with this specie");

        return true;
    }
}
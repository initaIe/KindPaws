using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.HardDelete;

public class
    HardDeletePetEntitiesExistenceValidator : IEntitiesExistenceValidator<HardDeletePetExistenceValidationData>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public HardDeletePetEntitiesExistenceValidator(IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<Error>> ValidateAsync(
        HardDeletePetExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _readDbContext.Volunteers.AnyAsync(
            v => v.Id == validationData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), validationData.VolunteerId);

        var isPetByIdExist = await _readDbContext.Pets.AnyAsync(
            p => p.Id == validationData.PetId, cancellationToken);
        if (!isPetByIdExist)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), validationData.PetId);

        return true;
    }
}
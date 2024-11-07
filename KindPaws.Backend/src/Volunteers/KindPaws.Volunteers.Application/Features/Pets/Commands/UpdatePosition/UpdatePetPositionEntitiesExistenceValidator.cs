using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePosition;

public class UpdatePetPositionEntitiesExistenceValidator
    : IEntitiesExistenceValidator<UpdatePetPositionExistenceValidationData>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public UpdatePetPositionEntitiesExistenceValidator(IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<Error>> ValidateAsync(
        UpdatePetPositionExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _readDbContext.Volunteers.AnyAsync(
            v => v.Id == validationData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), validationData.VolunteerId);

        var isPetByIdForVolunteerById = await _readDbContext.Pets.AnyAsync(
            p => p.VolunteerId == validationData.VolunteerId && p.Id == validationData.PetId, cancellationToken);
        if (!isPetByIdForVolunteerById)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), validationData.PetId);

        return true;
    }
}
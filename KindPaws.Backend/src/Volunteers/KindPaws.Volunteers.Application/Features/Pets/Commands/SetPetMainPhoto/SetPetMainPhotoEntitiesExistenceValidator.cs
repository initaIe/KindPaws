using KindPaws.Core.Abstractions.Validators;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Abstractions;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.SetMainPhoto;

public class SetPetMainPhotoEntitiesExistenceValidator
    : IEntitiesExistenceValidator<SetPetMainPhotoExistenceValidationData>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public SetPetMainPhotoEntitiesExistenceValidator(IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<Error>> ValidateAsync(
        SetPetMainPhotoExistenceValidationData validationData,
        CancellationToken cancellationToken = default)
    {
        var isVolunteerByIdExist = await _readDbContext.Volunteers.AnyAsync(
            v => v.Id == validationData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), validationData.VolunteerId);

        var isPetByIdForVolunteerByIdExist = await _readDbContext.Pets.AnyAsync(
            p => p.VolunteerId == validationData.VolunteerId && p.Id == validationData.PetId, cancellationToken);
        if (!isPetByIdForVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), validationData.PetId);

        return true;
    }
}
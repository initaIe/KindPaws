using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.AddPhotos;

public class AddPetPhotosEntitiesExistenceValidator : IEntitiesExistenceValidator<AddPetPhotosExistenceValidationData>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public AddPetPhotosEntitiesExistenceValidator(IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<Error>> ValidateAsync(
        AddPetPhotosExistenceValidationData validationData,
        CancellationToken cancellationToken)
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
using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateMainInfo;

public class UpdatePetMainInfoEntitiesExistenceValidator
    : IEntitiesExistenceValidator<UpdatePetMainInfoExistenceValidationData>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public UpdatePetMainInfoEntitiesExistenceValidator(IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<Error>> ValidateAsync(
        UpdatePetMainInfoExistenceValidationData validationData,
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

        // var isSpecieByIdExist = await _specieExistenceValidator
        //     .IsSpecieByIdExistsAsync(validationData.SpecieId, cancellationToken);
        // if (!isSpecieByIdExist)
        //     return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), validationData.SpecieId);
        //
        // var isBreedWithIdExistForSpecieWithId = await _breedExistenceValidator
        //     .IsBreedByIdForSpecieByIdExistsAsync(validationData.SpecieId, validationData.BreedId,
        //         cancellationToken);
        // if (!isBreedWithIdExistForSpecieWithId)
        //     return Errors.General.RecordNotFound(nameof(Breed), nameof(BreedId), validationData.BreedId);

        return true;
    }
}
using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Contracts;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateMainInfo;

public class UpdatePetMainInfoEntitiesExistenceValidator
    : IEntitiesExistenceValidator<UpdatePetMainInfoExistenceValidationData>
{
    private readonly IVolunteersReadDbContext _readDbContext;
    private readonly ISpeciesContract _speciesContract;

    public UpdatePetMainInfoEntitiesExistenceValidator(
        IVolunteersReadDbContext readDbContext,
        ISpeciesContract speciesContract)
    {
        _readDbContext = readDbContext;
        _speciesContract = speciesContract;
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

        var isSpecieByIdExist = await _speciesContract
            .IsSpecieByIdExistsAsync(validationData.SpecieId, cancellationToken);
        if (!isSpecieByIdExist)
            return Errors.General.RecordNotFound("Specie", "SpecieId", validationData.SpecieId);

        var isBreedByIdForSpecieByIdExist = await _speciesContract
            .IsBreedByIdForSpecieByIdExistsAsync(validationData.BreedId, validationData.SpecieId, cancellationToken);
        if (!isBreedByIdForSpecieByIdExist)
            return Errors.General.RecordNotFound("Breed", "BreedId", validationData.BreedId);

        return true;
    }
}
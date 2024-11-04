using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.AggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.Add;

public class AddPetEntitiesExistenceValidator : IEntitiesExistenceValidator<AddPetExistenceValidationData>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public AddPetEntitiesExistenceValidator(IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<Error>> ValidateAsync(
        AddPetExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _readDbContext.Volunteers.AnyAsync(
            v => v.Id == validationData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), validationData.VolunteerId);

        // var isSpecieByIdExist = await _specieExistenceValidator
        //     .IsSpecieByIdExistsAsync(validationData.SpecieId, cancellationToken);
        // if (!isSpecieByIdExist)
        //     return Errors.General.RecordNotFound(nameof(Specie), nameof(SpecieId), validationData.SpecieId);
        //
        // var isBreedByIdExistForSpecieWithId = await _breedExistenceValidator
        //     .IsBreedByIdForSpecieByIdExistsAsync(validationData.SpecieId, validationData.BreedId,
        //         cancellationToken);
        // if (!isBreedByIdExistForSpecieWithId)
        //     return Errors.General.RecordNotFound(nameof(Breed), nameof(BreedId), validationData.BreedId);

        return true;
    }
}
using KindPaws.Core.Abstractions.Validators;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.Species.Contracts;
using KindPaws.Volunteers.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.Add;

public class AddPetEntitiesExistenceValidator : IEntitiesExistenceValidator<AddPetExistenceValidationData>
{
    private readonly IVolunteersReadDbContext _readDbContext;
    private readonly ISpeciesContract _speciesContract;

    public AddPetEntitiesExistenceValidator(
        IVolunteersReadDbContext readDbContext,
        ISpeciesContract speciesContract)
    {
        _readDbContext = readDbContext;
        _speciesContract = speciesContract;
    }

    public async Task<Result<Error>> ValidateAsync(
        AddPetExistenceValidationData validationData,
        CancellationToken cancellationToken = default)
    {
        var isVolunteerByIdExist = await _readDbContext.Volunteers.AnyAsync(
            v => v.Id == validationData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound("Volunteer", "VolunteerId", validationData.VolunteerId);

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
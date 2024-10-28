using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.ExistValidators;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.AddPhotos;

public class AddPetPhotosEntitiesExistenceChecker : IEntitiesExistenceChecker<AddPetPhotosExistenceCheckData>
{
    private readonly IPetExistValidator _petExistValidator;
    private readonly IVolunteerExistValidator _volunteerExistValidator;

    public AddPetPhotosEntitiesExistenceChecker(
        IVolunteerExistValidator volunteerExistValidator,
        IPetExistValidator petExistValidator)
    {
        _volunteerExistValidator = volunteerExistValidator;
        _petExistValidator = petExistValidator;
    }

    public async Task<Result<Error>> CheckAsync(
        AddPetPhotosExistenceCheckData checkData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _volunteerExistValidator
            .IsVolunteerByIdExistsAsync(checkData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), checkData.VolunteerId);

        var isPetByIdExist = await _petExistValidator
            .IsPetByIdExists(checkData.PetId, cancellationToken);
        if (!isPetByIdExist)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), checkData.PetId);

        return true;
    }
}
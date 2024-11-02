using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.SetMainPhoto;

public record SetPetMainPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    string Path)
    : ICommand
{
    public SetPetMainPhotoExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}
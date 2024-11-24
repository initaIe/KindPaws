using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.SetPetMainPhoto;

public record SetPetMainPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    string Path)
    : ICommand
{
    public SetPetMainPhotoExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}
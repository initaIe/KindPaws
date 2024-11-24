using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.DeletePetPhotos;

public record DeletePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<string> PhotosPaths)
    : ICommand
{
    public DeletePetPhotosExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}
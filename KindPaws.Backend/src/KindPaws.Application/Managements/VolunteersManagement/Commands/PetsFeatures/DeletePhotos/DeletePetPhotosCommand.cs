using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.DeletePhotos;

public record DeletePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<string> PhotosPaths)
    : ICommand
{
    public DeletePetPhotosExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}
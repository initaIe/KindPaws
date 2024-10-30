using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.DTOs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.DeletePhotos;

public record DeletePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<DeleteFileDTO> DeleteFileDtos)
    : ICommand
{
    public DeletePetPhotosExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}
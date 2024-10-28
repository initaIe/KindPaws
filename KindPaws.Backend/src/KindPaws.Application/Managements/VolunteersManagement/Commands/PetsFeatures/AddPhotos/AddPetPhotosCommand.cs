using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.DTOs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.AddPhotos;

public record AddPetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDTO> UploadFileDtos)
    : ICommand
{
    public AddPetPhotosExistenceCheckData ToExistenceCheckData()
        => new(VolunteerId, PetId);
}
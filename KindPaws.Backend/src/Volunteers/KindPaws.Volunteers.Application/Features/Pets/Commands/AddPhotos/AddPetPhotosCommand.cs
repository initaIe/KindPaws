using KindPaws.Core.Abstractions.Markers;
using KindPaws.Core.Dtos;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.AddPhotos;

public record AddPetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDto> UploadFileDtos)
    : ICommand
{
    public AddPetPhotosExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}
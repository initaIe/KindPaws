using KindPaws.Application.Volunteers.DTOs;

namespace KindPaws.Application.Volunteers.PetHandlers.AddPhotos;

public record AddPetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<FileDTO> PhotoFileDtos);
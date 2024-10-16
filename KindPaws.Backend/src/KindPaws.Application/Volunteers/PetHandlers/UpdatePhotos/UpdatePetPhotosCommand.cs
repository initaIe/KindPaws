using KindPaws.Application.Volunteers.DTOs;

namespace KindPaws.Application.Volunteers.PetHandlers.UpdatePhotos;

public record UpdatePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<FileDTO> Photos);
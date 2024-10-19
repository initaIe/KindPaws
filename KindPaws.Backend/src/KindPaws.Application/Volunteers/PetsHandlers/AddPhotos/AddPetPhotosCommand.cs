using KindPaws.Application.DTOs;

namespace KindPaws.Application.Volunteers.PetsHandlers.AddPhotos;

public record AddPetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDTO> UploadFileDtos);
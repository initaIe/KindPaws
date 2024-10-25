using KindPaws.Application.DTOs;
using ICommand = KindPaws.Application.Abstractions.ICommand;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.AddPhotos;

public record AddPetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDTO> UploadFileDtos)
    : ICommand;
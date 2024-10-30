using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.DeletePhotos;

namespace KindPaws.API.Controllers.Volunteers.Requests;

public record DeletePetPhotosRequest(IEnumerable<DeleteFileDTO> DeleteFileDtos)

{
    public DeletePetPhotosCommand ToCommand(Guid id, Guid petId)
        => new(id, petId, DeleteFileDtos);
}
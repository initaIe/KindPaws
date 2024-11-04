using KindPaws.Volunteers.Application.Features.Pets.Commands.DeletePhotos;

namespace KindPaws.Volunteers.Presentation.Volunteers.Requests;

public record DeletePetPhotosRequest(IEnumerable<string> PhotosPaths)

{
    public DeletePetPhotosCommand ToCommand(Guid id, Guid petId)
        => new(id, petId, PhotosPaths);
}
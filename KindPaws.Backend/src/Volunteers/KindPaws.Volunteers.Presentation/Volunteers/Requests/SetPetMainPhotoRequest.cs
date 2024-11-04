using KindPaws.Volunteers.Application.Features.Pets.Commands.SetMainPhoto;

namespace KindPaws.Volunteers.Presentation.Volunteers.Requests;

public record SetPetMainPhotoRequest(string Path)
{
    public SetPetMainPhotoCommand ToCommand(Guid volunteerId, Guid petId)
        => new(volunteerId, petId, Path);
}
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.SetMainPhoto;

namespace KindPaws.API.Controllers.Volunteers.Requests;

public record SetPetMainPhotoRequest(string Path)
{
    public SetPetMainPhotoCommand ToCommand(Guid volunteerId, Guid petId)
        => new(volunteerId, petId, Path);
}
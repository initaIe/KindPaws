using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdatePosition;

namespace KindPaws.API.Controllers.Volunteers.Requests;

public record UpdatePetPositionRequest(int Position)
{
    public UpdatePetPositionCommand ToCommand(Guid volunteerId, Guid petId)
        => new(volunteerId, petId, Position);
}
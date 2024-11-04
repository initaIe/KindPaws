using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePosition;

namespace KindPaws.Volunteers.Presentation.Volunteers.Requests;

public record UpdatePetPositionRequest(int Position)
{
    public UpdatePetPositionCommand ToCommand(Guid volunteerId, Guid petId)
        => new(volunteerId, petId, Position);
}
using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePosition;

public record UpdatePetPositionCommand(
    Guid VolunteerId,
    Guid PetId,
    int Position)
    : ICommand
{
    public UpdatePetPositionExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}
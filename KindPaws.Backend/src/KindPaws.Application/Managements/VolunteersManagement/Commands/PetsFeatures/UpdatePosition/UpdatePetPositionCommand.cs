using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdatePosition;

public record UpdatePetPositionCommand(
    Guid VolunteerId,
    Guid PetId,
    int Position)
    : ICommand
{
    public UpdatePetPositionExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}
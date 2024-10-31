using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.HardDelete;

public record HardDeletePetCommand(
    Guid VolunteerId,
    Guid PetId)
    : ICommand
{
    public HardDeletePetExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}
using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.SoftDelete;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.HardDelete;

public record HardDeletePetCommand(
    Guid VolunteerId,
    Guid PetId)
    : ICommand
{
    public HardDeletePetExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}
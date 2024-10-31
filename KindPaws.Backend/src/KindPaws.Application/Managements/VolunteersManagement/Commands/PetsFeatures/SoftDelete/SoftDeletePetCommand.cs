using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.SoftDelete;

public record SoftDeletePetCommand(
    Guid VolunteerId,
    Guid PetId)
    : ICommand
{
    public SoftDeletePetExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}
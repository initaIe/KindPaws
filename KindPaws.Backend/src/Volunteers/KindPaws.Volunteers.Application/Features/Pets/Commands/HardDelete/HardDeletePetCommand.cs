using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.HardDelete;

public record HardDeletePetCommand(
    Guid VolunteerId,
    Guid PetId)
    : ICommand
{
    public HardDeletePetExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}
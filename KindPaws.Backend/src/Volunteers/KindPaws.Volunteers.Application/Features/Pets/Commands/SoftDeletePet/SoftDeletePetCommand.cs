using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.SoftDeletePet;

public record SoftDeletePetCommand(
    Guid VolunteerId,
    Guid PetId)
    : ICommand
{
    public SoftDeletePetExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}
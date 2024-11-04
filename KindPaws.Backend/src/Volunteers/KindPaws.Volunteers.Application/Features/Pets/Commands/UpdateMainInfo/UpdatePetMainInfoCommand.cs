using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateMainInfo;

public record UpdatePetMainInfoCommand(
    Guid VolunteerId,
    Guid PetId,
    Guid SpecieId,
    Guid BreedId,
    string Name)
    : ICommand
{
    public UpdatePetMainInfoExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId, SpecieId, BreedId);
}
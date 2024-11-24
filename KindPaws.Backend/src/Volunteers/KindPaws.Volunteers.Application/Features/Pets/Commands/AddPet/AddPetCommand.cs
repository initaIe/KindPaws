using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.AddPet;

public record AddPetCommand(
    Guid VolunteerId,
    Guid SpecieId,
    Guid BreedId,
    string Name)
    : ICommand
{
    public AddPetExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, SpecieId, BreedId);
}
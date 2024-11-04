using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.Add;

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
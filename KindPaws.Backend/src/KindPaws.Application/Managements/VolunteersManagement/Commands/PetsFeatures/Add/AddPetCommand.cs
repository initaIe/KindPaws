using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.Add;

public record AddPetCommand(
    Guid VolunteerId,
    Guid SpecieId,
    Guid BreedId,
    string Name)
    : ICommand
{
    public AddPetExistenceCheckData ToExistenceCheckData()
        => new(VolunteerId, SpecieId, BreedId);
}
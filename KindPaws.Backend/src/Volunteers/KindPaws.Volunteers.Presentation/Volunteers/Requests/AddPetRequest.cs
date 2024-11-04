using KindPaws.Volunteers.Application.Features.Pets.Commands.Add;

namespace KindPaws.Volunteers.Presentation.Volunteers.Requests;

public record AddPetRequest(
    Guid SpecieId,
    Guid BreedId,
    string Name)
{
    public AddPetCommand ToCommand(Guid id)
        => new(id, SpecieId, BreedId, Name);
}
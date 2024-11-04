using KindPaws.Species.Application.Features.Breeds.Commands.Add;

namespace KindPaws.Species.Presentation.Species.Requests;

public record AddBreedRequest(
    string Name,
    string Description)
{
    public AddBreedCommand ToCommand(Guid id)
        => new(id, Name, Description);
}
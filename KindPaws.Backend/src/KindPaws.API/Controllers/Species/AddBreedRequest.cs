using KindPaws.Application.Species.BreedsHandlers.Add;

namespace KindPaws.API.Controllers.Species;

public record AddBreedRequest(
    string Name,
    string Description)
{
    public AddBreedCommand ToCommand(Guid id)
    {
        return new AddBreedCommand(
            id,
            Name,
            Description);
    }
}
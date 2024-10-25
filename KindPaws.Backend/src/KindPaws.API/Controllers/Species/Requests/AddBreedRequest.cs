using KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;

namespace KindPaws.API.Controllers.Species.Requests;

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
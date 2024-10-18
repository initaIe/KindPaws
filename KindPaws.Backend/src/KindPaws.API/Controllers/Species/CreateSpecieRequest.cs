using KindPaws.Application.Species.SpeciesHandlers.Create;

namespace KindPaws.API.Controllers.Species;

public record CreateSpecieRequest(
    string Name,
    string Description)
{
    public CreateSpecieCommand ToCommand()
    {
        return new CreateSpecieCommand(Name, Description);
    }
}
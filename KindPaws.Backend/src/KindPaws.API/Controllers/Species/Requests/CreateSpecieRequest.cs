using KindPaws.Application.Managements.SpeciesManagment.Commands.SpeciesFeatures.Create;

namespace KindPaws.API.Controllers.Species.Requests;

public record CreateSpecieRequest(
    string Name,
    string Description)
{
    public CreateSpecieCommand ToCommand()
    {
        return new CreateSpecieCommand(Name, Description);
    }
}
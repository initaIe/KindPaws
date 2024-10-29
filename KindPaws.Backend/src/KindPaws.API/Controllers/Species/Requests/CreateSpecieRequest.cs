using KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Create;

namespace KindPaws.API.Controllers.Species.Requests;

public record CreateSpecieRequest(
    string Name,
    string Description)
{
    public CreateSpecieCommand ToCommand()
        => new(Name, Description);
}
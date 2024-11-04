using KindPaws.Species.Application.Features.Species.Commands.Create;

namespace KindPaws.Species.Presentation.Species.Requests;

public record CreateSpecieRequest(
    string Name,
    string Description)
{
    public CreateSpecieCommand ToCommand()
        => new(Name, Description);
}
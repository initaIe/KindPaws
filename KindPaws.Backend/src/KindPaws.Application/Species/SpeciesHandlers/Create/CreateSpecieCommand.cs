namespace KindPaws.Application.Species.SpeciesHandlers.Create;

public record CreateSpecieCommand(
    string Name,
    string Description);
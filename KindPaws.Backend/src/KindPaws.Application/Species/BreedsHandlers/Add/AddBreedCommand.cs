namespace KindPaws.Application.Species.BreedsHandlers.Add;

public record AddBreedCommand(
    Guid SpecieId,
    string Name,
    string Description);
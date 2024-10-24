namespace KindPaws.Application.Managements.SpeciesManagment.Commands.BreedsFeatures.Add;

public record AddBreedCommand(
    Guid SpecieId,
    string Name,
    string Description);
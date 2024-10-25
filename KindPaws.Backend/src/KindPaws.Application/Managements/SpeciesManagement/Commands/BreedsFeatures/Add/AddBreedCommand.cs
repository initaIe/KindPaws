using KindPaws.Application.Abstractions;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;

public record AddBreedCommand(
    Guid SpecieId,
    string Name,
    string Description)
    : ICommand;
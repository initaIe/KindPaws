using KindPaws.Application.Abstractions;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Create;

public record CreateSpecieCommand(
    string Name,
    string Description)
    : ICommand;
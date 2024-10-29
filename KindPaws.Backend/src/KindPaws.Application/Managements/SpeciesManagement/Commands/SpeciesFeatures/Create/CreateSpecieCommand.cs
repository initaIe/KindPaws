using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Create;

public record CreateSpecieCommand(
    string Name,
    string Description)
    : ICommand
{
    public CreateSpecieExistenceValidationData ToExistenceValidationData()
        => new(Name);
}
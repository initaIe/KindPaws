using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Species.Commands.CreateSpecie;

public record CreateSpecieCommand(
    string Name,
    string Description)
    : ICommand
{
    public CreateSpecieExistenceValidationData ToExistenceValidationData()
        => new(Name);
}
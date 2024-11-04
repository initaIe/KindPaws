using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Breeds.Commands.Add;

public record AddBreedCommand(
    Guid SpecieId,
    string Name,
    string Description)
    : ICommand
{
    public AddBreedExistenceValidationData ToExistenceValidationData()
        => new(SpecieId, Name);
}
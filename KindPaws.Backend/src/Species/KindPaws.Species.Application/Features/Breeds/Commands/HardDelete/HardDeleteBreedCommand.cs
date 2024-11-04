using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Breeds.Commands.HardDelete;

public record HardDeleteBreedCommand(
    Guid SpecieId,
    Guid BreedId)
    : ICommand
{
    public HardDeleteBreedExistenceValidationData ToExistenceValidationData()
        => new(SpecieId, BreedId);
}
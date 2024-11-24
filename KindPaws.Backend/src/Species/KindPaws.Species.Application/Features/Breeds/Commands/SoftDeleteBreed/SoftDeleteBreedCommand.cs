using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Breeds.Commands.SoftDelete;

public record SoftDeleteBreedCommand(
    Guid SpecieId,
    Guid BreedId)
    : ICommand
{
    public SoftDeleteBreedExistenceValidationData ToExistenceValidationData()
        => new(SpecieId, BreedId);
}
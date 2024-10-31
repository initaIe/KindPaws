using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.HardDelete;

public record HardDeleteBreedCommand(
    Guid SpecieId,
    Guid BreedId)
    : ICommand
{
    public HardDeleteBreedExistenceValidationData ToExistenceValidationData()
        => new(SpecieId, BreedId);
}
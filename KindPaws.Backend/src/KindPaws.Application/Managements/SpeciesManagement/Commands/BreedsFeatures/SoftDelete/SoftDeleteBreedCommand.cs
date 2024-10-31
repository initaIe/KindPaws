using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.SoftDelete;

public record SoftDeleteBreedCommand(
    Guid SpecieId,
    Guid BreedId)
    : ICommand
{
    public SoftDeleteBreedExistenceValidationData ToExistenceValidationData()
        => new(SpecieId, BreedId);
}
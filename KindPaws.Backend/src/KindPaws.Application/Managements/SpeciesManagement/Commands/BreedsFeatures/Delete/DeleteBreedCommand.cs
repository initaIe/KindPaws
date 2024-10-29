using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Delete;

public record DeleteBreedCommand(
    Guid SpecieId,
    Guid BreedId)
    : ICommand
{
    public DeleteBreedExistenceValidationData ToExistenceValidationData()
        => new(SpecieId, BreedId);
}
using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Delete;

public record DeleteBreedExistenceValidationData(
    Guid SpecieId,
    Guid BreedId)
    : IExistenceValidationData;
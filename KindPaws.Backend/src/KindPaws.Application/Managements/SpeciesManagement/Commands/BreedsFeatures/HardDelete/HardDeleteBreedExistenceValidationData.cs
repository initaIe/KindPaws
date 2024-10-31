using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.HardDelete;

public record HardDeleteBreedExistenceValidationData(
    Guid SpecieId,
    Guid BreedId)
    : IExistenceValidationData;
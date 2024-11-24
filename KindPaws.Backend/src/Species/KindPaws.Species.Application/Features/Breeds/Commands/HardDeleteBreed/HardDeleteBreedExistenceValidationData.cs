using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Breeds.Commands.HardDeleteBreed;

public record HardDeleteBreedExistenceValidationData(
    Guid SpecieId,
    Guid BreedId)
    : IExistenceValidationData;
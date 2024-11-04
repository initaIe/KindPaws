using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Breeds.Commands.HardDelete;

public record HardDeleteBreedExistenceValidationData(
    Guid SpecieId,
    Guid BreedId)
    : IExistenceValidationData;
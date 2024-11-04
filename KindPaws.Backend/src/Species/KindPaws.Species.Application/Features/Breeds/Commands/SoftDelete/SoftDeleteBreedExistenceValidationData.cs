using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Breeds.Commands.SoftDelete;

public record SoftDeleteBreedExistenceValidationData(
    Guid SpecieId,
    Guid BreedId)
    : IExistenceValidationData;
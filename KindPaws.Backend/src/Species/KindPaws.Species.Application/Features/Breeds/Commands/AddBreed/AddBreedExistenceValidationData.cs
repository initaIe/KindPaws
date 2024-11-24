using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Breeds.Commands.Add;

public record AddBreedExistenceValidationData(
    Guid SpeciesId,
    string BreedName)
    : IExistenceValidationData;
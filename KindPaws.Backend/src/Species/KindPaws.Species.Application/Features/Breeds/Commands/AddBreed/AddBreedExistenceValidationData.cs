using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Breeds.Commands.AddBreed;

public record AddBreedExistenceValidationData(
    Guid SpeciesId,
    string BreedName)
    : IExistenceValidationData;
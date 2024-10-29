using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;

public record AddBreedExistenceValidationData(
    Guid SpeciesId,
    string BreedName)
    : IExistenceValidationData;
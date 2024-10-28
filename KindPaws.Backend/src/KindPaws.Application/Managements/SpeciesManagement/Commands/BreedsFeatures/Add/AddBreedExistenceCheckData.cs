using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;

public record AddBreedExistenceCheckData(
    Guid SpeciesId,
    string BreedName)
    : IExistenceCheckData;
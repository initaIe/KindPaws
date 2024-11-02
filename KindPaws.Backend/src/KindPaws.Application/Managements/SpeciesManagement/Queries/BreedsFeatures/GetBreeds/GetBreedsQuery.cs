using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures.GetBreeds;

public record GetBreedsQuery(
    int PageNumber,
    int PageSize,
    Guid? SpecieId,
    string? Name)
    : IQuery;
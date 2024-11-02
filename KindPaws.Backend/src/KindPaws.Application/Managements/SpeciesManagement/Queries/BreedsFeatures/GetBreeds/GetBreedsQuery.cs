using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures.GetBreeds;

public record GetBreedsQuery(
    int PageNumber,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    Guid? SpecieId,
    string? Name)
    : IQuery;
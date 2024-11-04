using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Species.Queries.GetSpecies;

public record GetSpeciesQuery(
    int PageNumber,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    string? Name)
    : IQuery;
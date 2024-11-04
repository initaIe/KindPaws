using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Breeds.Queries.GetBreeds;

public record GetBreedsQuery(
    int PageNumber,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    Guid? SpecieId,
    string? Name)
    : IQuery;
using KindPaws.Species.Application.Features.Species.Queries.GetSpecies;

namespace KindPaws.Species.Presentation.Breeds.Requests;

public record GetSpeciesRequest(
    int PageNumber,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    string? Name)
{
    public GetSpeciesQuery ToQuery()
        => new(
            PageNumber,
            PageSize,
            SortBy,
            SortDirection,
            Name);
}
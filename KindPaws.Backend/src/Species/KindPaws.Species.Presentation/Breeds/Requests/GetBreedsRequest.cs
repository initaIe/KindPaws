using KindPaws.Species.Application.Features.Breeds.Queries.GetBreeds;

namespace KindPaws.Species.Presentation.Breeds.Requests;

public record GetBreedsRequest(
    int PageNumber,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    Guid? SpecieId,
    string? Name)
{
    public GetBreedsQuery ToQuery()
        => new(
            PageNumber,
            PageSize,
            SortBy,
            SortDirection,
            SpecieId,
            Name);
}
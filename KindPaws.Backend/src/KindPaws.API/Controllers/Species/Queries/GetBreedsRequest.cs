using KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures.GetBreeds;

namespace KindPaws.API.Controllers.Species.Queries;

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
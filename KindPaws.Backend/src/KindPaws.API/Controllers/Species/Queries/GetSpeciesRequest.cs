using KindPaws.Application.Managements.SpeciesManagement.Queries.SpeciesFeatures.GetSpecies;

namespace KindPaws.API.Controllers.Species.Queries;

public record GetSpeciesRequest(
    int PageNumber,
    int PageSize,
    string? Name)
{
    public GetSpeciesQuery ToQuery()
        => new(
            PageNumber,
            PageSize,
            Name);
}
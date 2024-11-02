using KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures.GetBreeds;

namespace KindPaws.API.Controllers.Species.Queries;

public record GetBreedsRequest(
    int PageNumber,
    int PageSize,
    Guid? SpecieId,
    string? Name)
{
    public GetBreedsQuery ToQuery()
        => new(
            PageNumber,
            PageSize,
            SpecieId,
            Name);
}
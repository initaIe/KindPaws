using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures;

namespace KindPaws.API.Controllers.Species.Queries;

public record GetBreedsRequest(
    PaginationDTO Pagination,
    Guid? SpecieId,
    string? Name)
{
    public GetBreedsQuery ToQuery()
        => new(Pagination, SpecieId, Name);
}
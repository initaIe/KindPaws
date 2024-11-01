using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures;

namespace KindPaws.API.Controllers.Species.Queries;

public record GetBreedsWithPaginationAndFilterRequest(
    PaginationDTO Pagination,
    Guid? SpecieId,
    string? Name)
{
    public GetBreedsWithPaginationAndFilterQuery ToQuery()
        => new(Pagination, SpecieId, Name);
}
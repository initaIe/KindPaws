using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.SpeciesManagement.Queries.SpeciesFeatures;

namespace KindPaws.API.Controllers.Species.Queries;

public record GetSpeciesWithPaginationAndFilterRequest(
    PaginationDTO Pagination,
    string? Name)
{
    public GetSpeciesWithPaginationAndFilterQuery ToQuery()
        => new(Pagination, Name);
}
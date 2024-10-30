using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.SpeciesManagement.Queries.SpeciesFeatures;

namespace KindPaws.API.Controllers.Species.Queries;

public record GetSpeciesWithPaginationRequest(PaginationDTO Pagination)
{
    public GetSpeciesWithPaginationQuery ToQuery()
        => new(Pagination);
}
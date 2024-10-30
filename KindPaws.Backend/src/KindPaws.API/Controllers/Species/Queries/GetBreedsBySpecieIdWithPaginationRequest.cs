using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures;

namespace KindPaws.API.Controllers.Species.Queries;

public record GetBreedsBySpecieIdWithPaginationRequest(
    Guid SpecieId,
    PaginationDTO Pagination)
{
    public GetBreedsBySpecieIdWithPaginationQuery ToQuery()
        => new(SpecieId, Pagination);
}
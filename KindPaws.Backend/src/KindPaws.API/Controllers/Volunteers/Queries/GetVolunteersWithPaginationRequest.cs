using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteersWithPagination;

namespace KindPaws.API.Controllers.Volunteers.Queries;

public record GetVolunteersWithPaginationRequest(PaginationDTO Pagination)
{
    public GetVolunteersWithPaginationQuery ToQuery()
        => new(Pagination);
}
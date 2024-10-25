using KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteersWithPagination;

namespace KindPaws.API.Controllers.Volunteers.Queries;

public record GetVolunteersWithPaginationRequest(
    int PageNumber,
    int PageSize)
{
    public GetVolunteersWithPaginationQuery ToQuery() =>
        new(PageNumber, PageSize);
}
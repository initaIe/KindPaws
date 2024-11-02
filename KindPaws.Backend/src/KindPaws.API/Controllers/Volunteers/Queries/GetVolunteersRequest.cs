using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteersWithPagination;

namespace KindPaws.API.Controllers.Volunteers.Queries;

public record GetVolunteersRequest(
    PaginationDTO Pagination,
    string? FirstName,
    string? LastName,
    string? Patronymic)
{
    public GetVolunteersQuery ToQuery()
        => new(Pagination, FirstName, LastName, Patronymic);
}
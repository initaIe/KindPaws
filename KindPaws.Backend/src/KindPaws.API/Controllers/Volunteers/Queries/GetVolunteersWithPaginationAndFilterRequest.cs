using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteersWithPagination;

namespace KindPaws.API.Controllers.Volunteers.Queries;

public record GetVolunteersWithPaginationAndFilterRequest(
    PaginationDTO Pagination,
    string? FirstName,
    string? LastName,
    string? Patronymic)
{
    public GetVolunteersWithPaginationAndFilterQuery ToQuery()
        => new(Pagination, FirstName, LastName, Patronymic);
}
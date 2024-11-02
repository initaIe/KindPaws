using KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteers;

namespace KindPaws.API.Controllers.Volunteers.Queries;

public record GetVolunteersRequest(
    int PageNumber,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    string? FirstName,
    string? LastName,
    string? Patronymic)
{
    public GetVolunteersQuery ToQuery()
        => new(
            PageNumber,
            PageSize,
            SortBy,
            SortDirection,
            FirstName,
            LastName,
            Patronymic);
}
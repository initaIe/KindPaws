using KindPaws.Volunteers.Application.Features.Volunteers.Queries.GetVolunteers;

namespace KindPaws.Volunteers.Presentation.Volunteers.Requests;

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
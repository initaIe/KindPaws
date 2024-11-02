using KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteers;

namespace KindPaws.API.Controllers.Volunteers.Queries;

public record GetVolunteersRequest(
    int PageNumber,
    int PageSize,
    string? FirstName,
    string? LastName,
    string? Patronymic)
{
    public GetVolunteersQuery ToQuery()
        => new(
            PageNumber,
            PageSize,
            FirstName,
            LastName,
            Patronymic);
}
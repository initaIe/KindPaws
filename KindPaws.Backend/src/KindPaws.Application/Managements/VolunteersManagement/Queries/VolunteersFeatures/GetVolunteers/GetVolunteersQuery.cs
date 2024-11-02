using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteers;

public record GetVolunteersQuery(
    int PageNumber,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    string? FirstName,
    string? LastName,
    string? Patronymic)
    : IQuery;
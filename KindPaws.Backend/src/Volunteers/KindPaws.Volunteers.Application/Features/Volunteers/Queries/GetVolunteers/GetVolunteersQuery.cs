using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Queries.GetVolunteers;

public record GetVolunteersQuery(
    int PageNumber,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    string? FirstName,
    string? LastName,
    string? Patronymic)
    : IQuery;
using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.DTOs;

namespace KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationAndFilterQuery(
    PaginationDTO Pagination, 
    string? FirstName,
    string? LastName, 
    string? Patronymic)
    : IQuery;
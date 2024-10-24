namespace KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationQuery(
    int PageNumber,
    int PageSize);
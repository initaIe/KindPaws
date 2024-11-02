using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.DTOs;
using KindPaws.Application.Extensions;
using KindPaws.Application.Models;

namespace KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteersWithPagination;

public class GetVolunteersHandler
    : IQueryHandler<PagedList<VolunteerDTO>, GetVolunteersQuery>
{
    // private readonly ILogger<GetVolunteersWithPaginationHandler> _logger;
    // private readonly IValidator<GetVolunteersWithPaginationQuery> _validator;
    private readonly IReadDbContext _readDbContext;

    public GetVolunteersHandler(
        // ILogger<GetVolunteersWithPaginationHandler> logger,
        // IValidator<GetVolunteersWithPaginationQuery> validator,
        IReadDbContext readDbContext)
    {
        // _logger = logger;
        // _validator = validator;
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<VolunteerDTO>> HandleAsync(
        GetVolunteersQuery query,
        CancellationToken cancellationToken)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        volunteersQuery = volunteersQuery.WhereIf(
            query.FirstName != null,
            v => v.FullName.FirstName.Contains(query.FirstName!));

        volunteersQuery = volunteersQuery.WhereIf(
            query.LastName != null,
            v => v.FullName.LastName.Contains(query.LastName!));

        volunteersQuery = volunteersQuery.WhereIf(
            query.Patronymic != null,
            v => v.FullName.Patronymic != null && v.FullName.Patronymic.Contains(query.LastName!));

        // TODO add validation, filtration, sort and logger

        return await volunteersQuery.ToPagedList(
            query.Pagination.PageNumber,
            query.Pagination.PageSize,
            cancellationToken);
    }
}
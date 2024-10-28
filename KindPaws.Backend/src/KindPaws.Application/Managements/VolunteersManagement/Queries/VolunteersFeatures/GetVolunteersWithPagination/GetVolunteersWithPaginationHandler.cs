using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.DTOs;
using KindPaws.Application.Extensions;
using KindPaws.Application.Models;

namespace KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationHandler
    : IQueryHandler<PagedList<VolunteerDTO>, GetVolunteersWithPaginationQuery>
{
    // private readonly ILogger<GetVolunteersWithPaginationHandler> _logger;
    // private readonly IValidator<GetVolunteersWithPaginationQuery> _validator;
    private readonly IReadDbContext _readDbContext;

    public GetVolunteersWithPaginationHandler(
        // ILogger<GetVolunteersWithPaginationHandler> logger,
        // IValidator<GetVolunteersWithPaginationQuery> validator,
        IReadDbContext readDbContext)
    {
        // _logger = logger;
        // _validator = validator;
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<VolunteerDTO>> HandleAsync(
        GetVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var volunteerQuery = _readDbContext.Volunteers;

        // TODO add validation, filtration, sort and logger

        return await volunteerQuery.ToPagedList(
            query.PageNumber,
            query.PageSize,
            cancellationToken);
    }
}
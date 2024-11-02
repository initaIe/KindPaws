using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.DTOs;
using KindPaws.Application.Extensions;
using KindPaws.Application.Models;

namespace KindPaws.Application.Managements.SpeciesManagement.Queries.SpeciesFeatures;

public class GetSpeciesWithPaginationAndFilterHandler
    : IQueryHandler<PagedList<SpecieDTO>, GetSpeciesWithPaginationAndFilterQuery>

{
    // private readonly ILogger<GetVolunteersWithPaginationHandler> _logger;
    // private readonly IValidator<GetVolunteersWithPaginationQuery> _validator;
    private readonly IReadDbContext _readDbContext;

    public GetSpeciesWithPaginationAndFilterHandler(
        // ILogger<GetVolunteersWithPaginationHandler> logger,
        // IValidator<GetVolunteersWithPaginationQuery> validator,
        IReadDbContext readDbContext)
    {
        // _logger = logger;
        // _validator = validator;
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<SpecieDTO>> HandleAsync(
        GetSpeciesWithPaginationAndFilterQuery query,
        CancellationToken cancellationToken)
    {
        var speciesQuery = _readDbContext.Species;

        speciesQuery = speciesQuery.WhereIf(
            query.Name != null,
            x => x.Name.Contains(query.Name!));

        // TODO add validation, filtration, sort and logger

        return await speciesQuery.ToPagedList(
            query.Pagination.PageNumber,
            query.Pagination.PageSize,
            cancellationToken);
    }
}
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.DTOs;
using KindPaws.Application.Extensions;
using KindPaws.Application.Models;

namespace KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures;

public class GetBreedsWithPaginationAndFilterHandler
    : IQueryHandler<PagedList<BreedDTO>, GetBreedsWithPaginationAndFilterQuery>
{
    // private readonly ILogger<GetVolunteersWithPaginationHandler> _logger;
    // private readonly IValidator<GetVolunteersWithPaginationQuery> _validator;
    private readonly IReadDbContext _readDbContext;

    public GetBreedsWithPaginationAndFilterHandler(
        // ILogger<GetVolunteersWithPaginationHandler> logger,
        // IValidator<GetVolunteersWithPaginationQuery> validator,
        IReadDbContext readDbContext)
    {
        // _logger = logger;
        // _validator = validator;
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<BreedDTO>> HandleAsync(
        GetBreedsWithPaginationAndFilterQuery query,
        CancellationToken cancellationToken)
    {
        var breedsQuery = _readDbContext.Breeds;

        breedsQuery.WhereIf(
            query.SpecieId != null,
            b => b.SpecieId == query.SpecieId!.Value);

        breedsQuery.WhereIf(
            query.Name != null,
            b => b.Name.Contains(query.Name!));

        // TODO add validation, filtration, sort and logger

        return await breedsQuery.ToPagedList(
            query.Pagination.PageNumber,
            query.Pagination.PageSize,
            cancellationToken);
    }
}
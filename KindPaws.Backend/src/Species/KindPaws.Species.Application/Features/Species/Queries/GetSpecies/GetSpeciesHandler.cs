using System.Linq.Expressions;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.Core.Models;
using KindPaws.Species.Application.Abstractions;
using KindPaws.Species.Application.DataModels;

namespace KindPaws.Species.Application.Features.Species.Queries.GetSpecies;

public class GetSpeciesHandler
    : IQueryHandler<PagedList<SpecieDataModel>, GetSpeciesQuery>
{
    private readonly ISpeciesReadDbContext _speciesReadDbContext;

    public GetSpeciesHandler(ISpeciesReadDbContext speciesReadDbContext)
    {
        _speciesReadDbContext = speciesReadDbContext;
    }

    public async Task<PagedList<SpecieDataModel>> HandleAsync(
        GetSpeciesQuery query,
        CancellationToken cancellationToken = default)
    {
        var speciesQuery = _speciesReadDbContext.Species;

        Expression<Func<SpecieDataModel, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "name" => (specie) => specie.Name,
            _ => (specie) => specie.Id
        };

        speciesQuery = query.SortDirection?.ToLower() == "desc"
            ? speciesQuery.OrderByDescending(keySelector)
            : speciesQuery.OrderBy(keySelector);

        speciesQuery = speciesQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Name),
            s => s.Name.Contains(query.Name!));

        return await speciesQuery.ToPagedList(
            query.PageNumber,
            query.PageSize,
            cancellationToken);
    }
}
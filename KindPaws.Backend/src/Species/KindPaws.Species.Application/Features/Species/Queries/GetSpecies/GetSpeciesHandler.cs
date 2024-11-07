using System.Linq.Expressions;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Dtos;
using KindPaws.Core.Extensions;
using KindPaws.Core.Models;
using KindPaws.Species.Application.Interfaces;

namespace KindPaws.Species.Application.Features.Species.Queries.GetSpecies;

public class GetSpeciesHandler
    : IQueryHandler<PagedList<SpecieDto>, GetSpeciesQuery>
{
    private readonly ISpeciesReadDbContext _speciesReadDbContext;

    public GetSpeciesHandler(ISpeciesReadDbContext speciesReadDbContext)
    {
        _speciesReadDbContext = speciesReadDbContext;
    }

    public async Task<PagedList<SpecieDto>> HandleAsync(
        GetSpeciesQuery query,
        CancellationToken cancellationToken)
    {
        var speciesQuery = _speciesReadDbContext.Species;

        Expression<Func<SpecieDto, object>> keySelector = query.SortBy?.ToLower() switch
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
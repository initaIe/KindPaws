using System.Linq.Expressions;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.DTOs;
using KindPaws.Application.Extensions;
using KindPaws.Application.Models;

namespace KindPaws.Application.Managements.SpeciesManagement.Queries.SpeciesFeatures.GetSpecies;

public class GetSpeciesHandler
    : IQueryHandler<PagedList<SpecieDTO>, GetSpeciesQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetSpeciesHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<SpecieDTO>> HandleAsync(
        GetSpeciesQuery query,
        CancellationToken cancellationToken)
    {
        var speciesQuery = _readDbContext.Species;

        Expression<Func<SpecieDTO, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "name" => (specie) => specie.Name,
            _ => (specie) => specie.Id
        };
        
        speciesQuery = query.SortDirection?.ToLower() == "descending"
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
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

        speciesQuery = speciesQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Name),
            x => x.Name.Contains(query.Name!));

        return await speciesQuery.ToPagedList(
            query.PageNumber,
            query.PageSize,
            cancellationToken);
    }
}
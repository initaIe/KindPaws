using System.Linq.Expressions;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Dtos;
using KindPaws.Core.Extensions;
using KindPaws.Core.Models;
using KindPaws.Species.Application.Interfaces;

namespace KindPaws.Species.Application.Features.Breeds.Queries.GetBreeds;

public class GetBreedsHandler
    : IQueryHandler<PagedList<BreedDto>, GetBreedsQuery>
{
    private readonly ISpeciesReadDbContext _readDbContext;

    public GetBreedsHandler(ISpeciesReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<BreedDto>> HandleAsync(
        GetBreedsQuery query,
        CancellationToken cancellationToken)
    {
        var breedsQuery = _readDbContext.Breeds;

        Expression<Func<BreedDto, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "name" => (breed) => breed.Name,
            "specieid" => (breed) => breed.SpecieId,
            _ => (breed) => breed.Id
        };

        breedsQuery = query.SortDirection?.ToLower() == "descending"
            ? breedsQuery.OrderByDescending(keySelector)
            : breedsQuery.OrderBy(keySelector);

        breedsQuery.WhereIf(
            query.SpecieId != null,
            b => b.SpecieId == query.SpecieId!.Value);

        breedsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Name),
            b => b.Name.Contains(query.Name!));

        return await breedsQuery.ToPagedList(
            query.PageNumber,
            query.PageSize,
            cancellationToken);
    }
}
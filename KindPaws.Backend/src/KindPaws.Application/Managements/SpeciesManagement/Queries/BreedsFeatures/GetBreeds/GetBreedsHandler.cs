using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.DTOs;
using KindPaws.Application.Extensions;
using KindPaws.Application.Models;

namespace KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures.GetBreeds;

public class GetBreedsHandler
    : IQueryHandler<PagedList<BreedDTO>, GetBreedsQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetBreedsHandler(
        IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<BreedDTO>> HandleAsync(
        GetBreedsQuery query,
        CancellationToken cancellationToken)
    {
        var breedsQuery = _readDbContext.Breeds;

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
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.DTOs;
using KindPaws.Application.Extensions;
using KindPaws.Application.Models;

namespace KindPaws.Application.Managements.VolunteersManagement.Queries.PetsFeatures.GetPets;

public class GetPetsHandler
    : IQueryHandler<PagedList<PetDTO>, GetPetsQuery>

{
    private readonly IReadDbContext _readDbContext;

    public GetPetsHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<PetDTO>> HandleAsync(
        GetPetsQuery query,
        CancellationToken cancellationToken)
    {
        var petsQuery = _readDbContext.Pets;
        
        petsQuery = petsQuery.WhereIf(
            query.SpecieId != null,
            p => p.SpecieId == query.SpecieId!.Value);
        
        petsQuery = petsQuery.WhereIf(
            query.BreedId != null,
            p => p.BreedId == query.BreedId!.Value);
        
        petsQuery = petsQuery.WhereIf(
            query.Name != null,
            p => p.Name.Contains(query.Name!));
        
        petsQuery = petsQuery.WhereIf(
            query.SupportStatus != null,
            p => p.SupportStatus != null && p.SupportStatus.Contains(query.Name!));
        
        petsQuery = petsQuery.WhereIf(
            query.Color != null,
            p => p.Color != null && p.Color.Contains(query.Name!));
        
        petsQuery = petsQuery.WhereIf(
            query.Age != null,
            p => p.Age != null && p.Age.Value == query.Age!.Value);
        
        petsQuery = petsQuery.WhereIf(
            query.VolunteerId != null,
            p => p.VolunteerId == query.VolunteerId!.Value);
        
        return await petsQuery.ToPagedList(
            query.Pagination.PageNumber,
            query.Pagination.PageSize,
            cancellationToken);
    }
}
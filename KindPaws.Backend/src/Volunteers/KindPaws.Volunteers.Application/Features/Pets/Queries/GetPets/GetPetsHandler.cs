using System.Linq.Expressions;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.Core.Models;
using KindPaws.Volunteers.Application.Abstractions;
using KindPaws.Volunteers.Application.DataModels;

namespace KindPaws.Volunteers.Application.Features.Pets.Queries.GetPets;

public class GetPetsHandler
    : IQueryHandler<PagedList<PetDataModel>, GetPetsQuery>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public GetPetsHandler(IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<PetDataModel>> HandleAsync(
        GetPetsQuery query,
        CancellationToken cancellationToken = default)
    {
        var petsQuery = _readDbContext.Pets;

        Expression<Func<PetDataModel, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "specieid" => pet => pet.SpecieId,
            "breedid" => pet => pet.BreedId,
            "name" => pet => pet.Name,
            "supportstatus" => pet => pet.SupportStatus!,
            "color" => pet => pet.Color!,
            "age" => pet => pet.Birthday!,
            "position" => pet => pet.Position,
            "volunteerid" => pet => pet.VolunteerId,
            _ => pet => pet.Id
        };

        petsQuery = query.SortDirection?.ToLower() == "descending"
            ? petsQuery.OrderByDescending(keySelector)
            : petsQuery.OrderBy(keySelector);

        petsQuery = petsQuery.WhereIf(
            query.SpecieId != null,
            p => p.SpecieId == query.SpecieId!.Value);

        petsQuery = petsQuery.WhereIf(
            query.BreedId != null,
            p => p.BreedId == query.BreedId!.Value);

        petsQuery = petsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Name),
            p => p.Name.Contains(query.Name!));

        petsQuery = petsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.SupportStatus),
            p => p.SupportStatus != null && p.SupportStatus.Contains(query.SupportStatus!));

        petsQuery = petsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Color),
            p => p.Color != null && p.Color.Contains(query.Color!));

        petsQuery = petsQuery.WhereIf(
            query.Age != null,
            p => p.Birthday != null && p.Birthday.Value == query.Age!.Value);

        petsQuery = petsQuery.WhereIf(
            query.VolunteerId != null,
            p => p.VolunteerId == query.VolunteerId!.Value);

        petsQuery = petsQuery.WhereIf(
            query.PositionFrom != null,
            p => p.Position >= query.PositionFrom!.Value);

        petsQuery = petsQuery.WhereIf(
            query.PositionTo != null,
            p => p.Position <= query.PositionTo!.Value);

        return await petsQuery.ToPagedList(
            query.PageNumber,
            query.PageSize,
            cancellationToken);
    }
}
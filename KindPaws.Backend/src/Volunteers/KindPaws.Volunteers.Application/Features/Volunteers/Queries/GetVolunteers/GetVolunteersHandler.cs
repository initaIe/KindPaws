using System.Linq.Expressions;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.Core.Models;
using KindPaws.Volunteers.Application.Abstractions;
using KindPaws.Volunteers.Application.DataModels;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Queries.GetVolunteers;

public class GetVolunteersHandler
    : IQueryHandler<PagedList<VolunteerDataModel>, GetVolunteersQuery>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public GetVolunteersHandler(
        IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<VolunteerDataModel>> HandleAsync(
        GetVolunteersQuery query,
        CancellationToken cancellationToken = default)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        Expression<Func<VolunteerDataModel, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "city" => volunteer => volunteer.Address!.City,
            "street" => volunteer => volunteer.Address!.Street,
            "yearsofexperience" => volunteer => volunteer.YearsOfExperience!,
            _ => volunteer => volunteer.Id
        };

        volunteersQuery = query.SortDirection?.ToLower() == "descending"
            ? volunteersQuery.OrderByDescending(keySelector)
            : volunteersQuery.OrderBy(keySelector);

        return await volunteersQuery.ToPagedList(
            query.PageNumber,
            query.PageSize,
            cancellationToken);
    }
}
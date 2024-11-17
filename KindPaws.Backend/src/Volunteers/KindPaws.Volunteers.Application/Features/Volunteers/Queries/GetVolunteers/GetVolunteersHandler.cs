using System.Linq.Expressions;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.Core.Models;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Contracts.Dtos;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Queries.GetVolunteers;

public class GetVolunteersHandler
    : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersQuery>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public GetVolunteersHandler(
        IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<VolunteerDto>> HandleAsync(
        GetVolunteersQuery query,
        CancellationToken cancellationToken = default)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        Expression<Func<VolunteerDto, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "firstname" => volunteer => volunteer.FullName.FirstName,
            "lastname" => volunteer => volunteer.FullName.LastName,
            "patronymic" => volunteer => volunteer.FullName.Patronymic!,
            "emailaddress" => volunteer => volunteer.EmailAddress,
            "phonenumber" => volunteer => volunteer.PhoneNumber,
            "city" => volunteer => volunteer.Address!.City,
            "street" => volunteer => volunteer.Address!.Street,
            "yearsofexperience" => volunteer => volunteer.YearsOfExperience!,
            _ => volunteer => volunteer.Id
        };

        volunteersQuery = query.SortDirection?.ToLower() == "descending"
            ? volunteersQuery.OrderByDescending(keySelector)
            : volunteersQuery.OrderBy(keySelector);

        volunteersQuery = volunteersQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.FirstName),
            v => v.FullName.FirstName.Contains(query.FirstName!));

        volunteersQuery = volunteersQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.LastName),
            v => v.FullName.LastName.Contains(query.LastName!));

        volunteersQuery = volunteersQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Patronymic),
            v => v.FullName.Patronymic != null && v.FullName.Patronymic.Contains(query.LastName!));

        return await volunteersQuery.ToPagedList(
            query.PageNumber,
            query.PageSize,
            cancellationToken);
    }
}
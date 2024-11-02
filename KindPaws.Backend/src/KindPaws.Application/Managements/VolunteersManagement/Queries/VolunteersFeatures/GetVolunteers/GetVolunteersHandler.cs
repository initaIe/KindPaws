using System.Linq.Expressions;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.DTOs;
using KindPaws.Application.Extensions;
using KindPaws.Application.Models;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteers;

public class GetVolunteersHandler
    : IQueryHandler<PagedList<VolunteerDTO>, GetVolunteersQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetVolunteersHandler(
        IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<VolunteerDTO>> HandleAsync(
        GetVolunteersQuery query,
        CancellationToken cancellationToken)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        Expression<Func<VolunteerDTO, object>> keySelector = query.SortBy?.ToLower() switch
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
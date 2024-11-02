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
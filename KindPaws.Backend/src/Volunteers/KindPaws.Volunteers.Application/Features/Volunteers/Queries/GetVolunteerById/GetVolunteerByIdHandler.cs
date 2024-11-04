using KindPaws.Core.Abstractions;
using KindPaws.Core.Dtos;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.AggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Queries.GetVolunteerById;

public class GetVolunteerByIdHandler
    : IQueryHandler<Result<VolunteerDto, ErrorList>, GetVolunteerByIdQuery>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public GetVolunteerByIdHandler(
        IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<VolunteerDto, ErrorList>> HandleAsync(
        GetVolunteerByIdQuery query,
        CancellationToken cancellationToken)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        var volunteerId = VolunteerId.Create(query.VolunteerId).Value;

        var volunteer = await volunteersQuery
            .SingleOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

        if (volunteer == null)
            return Errors.General.RecordNotFound(
                    nameof(Volunteer),
                    nameof(VolunteerId),
                    volunteerId.Value)
                .ToErrorList();

        return volunteer;
    }
}
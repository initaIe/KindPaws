using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Abstractions;
using KindPaws.Volunteers.Application.DataModels;
using KindPaws.Volunteers.Contracts.Dtos;
using KindPaws.Volunteers.Domain.AggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Queries.GetVolunteerById;

public class GetVolunteerByIdHandler
    : IQueryHandler<Result<VolunteerDataModel, ErrorList>, GetVolunteerByIdQuery>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public GetVolunteerByIdHandler(
        IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<VolunteerDataModel, ErrorList>> HandleAsync(
        GetVolunteerByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        var volunteerId = VolunteerId.Create(query.VolunteerId).Value;

        var volunteer = await volunteersQuery
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

        if (volunteer == null)
            return ErrorsGeneral.RecordNotFound(
                    nameof(Volunteer),
                    nameof(VolunteerId),
                    volunteerId.Value)
                .ToErrorList();

        return volunteer;
    }
}
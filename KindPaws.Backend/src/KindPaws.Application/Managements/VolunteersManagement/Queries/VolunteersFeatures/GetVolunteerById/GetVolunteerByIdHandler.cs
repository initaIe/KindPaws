using KindPaws.Application.Abstractions;
using KindPaws.Application.DTOs;
using KindPaws.Application.Extensions;
using KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteersWithPagination;
using KindPaws.Application.Models;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteerById;

public class GetVolunteerByIdHandler
{
    // private readonly ILogger<GetVolunteersWithPaginationHandler> _logger;
    // private readonly IValidator<GetVolunteersWithPaginationQuery> _validator;
    private readonly IReadDbContext _readDbContext;

    public GetVolunteerByIdHandler(
        // ILogger<GetVolunteersWithPaginationHandler> logger,
        // IValidator<GetVolunteersWithPaginationQuery> validator,
        IReadDbContext readDbContext)
    {
        // _logger = logger;
        // _validator = validator;
        _readDbContext = readDbContext;
    }

    public async Task<Result<VolunteerDTO, ErrorList>> HandleAsync(
        GetVolunteerByIdQuery query,
        CancellationToken cancellationToken)
    {
        var volunteerQuery = _readDbContext.Volunteers
            .AsQueryable()
            .AsNoTracking();
        
        // TODO add validation, filtration, sort and logger

        var volunteerId = VolunteerId.Create(query.VolunteerId).Value;

        var volunteer = await volunteerQuery
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

        if (volunteer == null)
            return Errors.General.RecordNotFound(
                nameof(Volunteer), 
                nameof(volunteerId), 
                volunteerId.Value)
                .ToErrorList();

        return volunteer;
    }
}
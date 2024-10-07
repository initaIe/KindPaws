using KindPaws.Application.Volunteers.Handlers.Create;
using KindPaws.Application.Volunteers.Handlers.GetById.DTOs;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.Handlers.GetById;

public class GetByIdVolunteerHandler
{
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;

    public GetByIdVolunteerHandler(ILogger<CreateVolunteerHandler> logger, IVolunteersRepository volunteersRepository)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Volunteer, Error>> HandleAsync(
        GetByIdVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.Create(request.VolunteerId).Value;

        var getVolunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        if (getVolunteerResult.IsFailure)
            return getVolunteerResult.Error;

        return getVolunteerResult.Value;
    }
}
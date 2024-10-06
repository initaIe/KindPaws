using KindPaws.Application.Volunteers.Create;
using KindPaws.Application.Volunteers.UpdateMainInfo.DTOs;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.UpdateMainInfo;

public class UpdateVolunteerMainInfoHandler
{
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;

    public UpdateVolunteerMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> HandleAsync(
        UpdateVolunteerMainInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.Create(request.ModuleId).Value;

        var volunteerResult = await _volunteersRepository.GetByIdAsync(
            volunteerId,
            cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        // volunteerResult.Value.UpdateMainInfo();


        return Guid.Empty;
    }
}
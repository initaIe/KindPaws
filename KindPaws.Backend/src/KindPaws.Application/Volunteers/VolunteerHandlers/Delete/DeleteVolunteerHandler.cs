using KindPaws.Application.Volunteers.VolunteerHandlers.Delete.DTOs;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.VolunteerHandlers.Delete;

public class DeleteVolunteerHandler
{
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;

    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<DeleteVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> HandleAsync(
        DeleteVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.Create(request.VolunteerId).Value;

        var volunteerResult = await _volunteersRepository.GetByIdAsync(
            volunteerId,
            cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var result = await _volunteersRepository.DeleteAsync(volunteerResult.Value, cancellationToken);

        _logger.LogInformation("VOLUNTEER soft deleted with ID: {VolunteerId}", volunteerId.Value);

        return result;
    }
}
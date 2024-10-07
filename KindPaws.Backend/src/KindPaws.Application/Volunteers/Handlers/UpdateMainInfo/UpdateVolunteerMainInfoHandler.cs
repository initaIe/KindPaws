using KindPaws.Application.Volunteers.Handlers.Create;
using KindPaws.Application.Volunteers.Handlers.UpdateMainInfo.DTOs;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.Handlers.UpdateMainInfo;

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
        var volunteerId = VolunteerId.Create(request.VolunteerId).Value;

        var volunteerResult = await _volunteersRepository.GetByIdAsync(
            volunteerId,
            cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var fullName = FullName.Create(
            request.Dto.FullName.FirstName,
            request.Dto.FullName.LastName,
            request.Dto.FullName.Patronymic).Value;

        var emailAddress = EmailAddress.Create(request.Dto.EmailAddress).Value;

        var phoneNumber = PhoneNumber.Create(request.Dto.PhoneNumber).Value;

        volunteerResult.Value.UpdateMainInfo(fullName, emailAddress, phoneNumber);

        var id = await _volunteersRepository.UpdateAsync(volunteerResult.Value, cancellationToken);

        _logger.LogInformation("Updated volunteer {VolunteerId}", volunteerId);

        return id;
    }
}
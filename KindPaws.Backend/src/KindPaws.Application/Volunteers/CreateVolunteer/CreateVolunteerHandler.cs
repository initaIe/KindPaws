using KindPaws.Application.Volunteers.CreateVolunteer.DTOs;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;

    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> HandleAsync(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var emailAddress = EmailAddress.Create(request.EmailAddress).Value;

        var existVolunteerWithEmailAddress =
            await _volunteersRepository.GetByEmailAddressAsync(emailAddress, cancellationToken);

        // volunteer email is already exist validation
        if (existVolunteerWithEmailAddress.IsSuccess)
            return Errors.General.RecordAlreadyExist(nameof(Volunteer));

        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;

        // volunteer phone number is already exist validation
        var existVolunteerWithPhoneNumber =
            await _volunteersRepository.GetByPhoneNumberAsync(phoneNumber, cancellationToken);

        if (existVolunteerWithPhoneNumber.IsSuccess)
            return Errors.General.RecordAlreadyExist(nameof(Volunteer));

        var volunteerId = VolunteerId.CreateRandom();

        var fullName = FullName.Create(
            request.FullName.FirstName,
            request.FullName.LastName,
            request.FullName.Patronymic).Value;

        var volunteerToCreate = new Volunteer(
            volunteerId,
            fullName,
            emailAddress,
            null,
            null,
            null,
            phoneNumber,
            null,
            null);

        await _volunteersRepository.AddAsync(volunteerToCreate, cancellationToken);

        _logger.LogInformation("Created volunteer with {VolunteerId}, {EmailAddress}",
            volunteerId, emailAddress);

        return (Guid)volunteerToCreate.Id;
    }
}
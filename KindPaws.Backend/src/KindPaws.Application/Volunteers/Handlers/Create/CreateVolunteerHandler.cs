using KindPaws.Application.Volunteers.Handlers.Create.DTOs;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.Handlers.Create;

public class CreateVolunteerHandler
{
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;

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

        var existVolunteerWithEmailAddressResult =
            await _volunteersRepository.GetByEmailAddressAsync(emailAddress, cancellationToken);

        // volunteer email is already exist validation
        if (existVolunteerWithEmailAddressResult.IsSuccess)
            return existVolunteerWithEmailAddressResult.Error;

        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;

        // volunteer phone number is already exist validation
        var existVolunteerWithPhoneNumberResult =
            await _volunteersRepository.GetByPhoneNumberAsync(phoneNumber, cancellationToken);

        if (existVolunteerWithPhoneNumberResult.IsSuccess)
            return existVolunteerWithPhoneNumberResult.Error;

        var volunteerId = VolunteerId.CreateRandom();

        var fullName = FullName.Create(
            request.FullName.FirstName,
            request.FullName.LastName,
            request.FullName.Patronymic).Value;

        var volunteerToCreate = new Volunteer(
            volunteerId,
            fullName,
            emailAddress,
            phoneNumber);

        await _volunteersRepository.AddAsync(volunteerToCreate, cancellationToken);

        _logger.LogInformation("Created volunteer {VolunteerId}", volunteerId);

        return (Guid)volunteerToCreate.Id;
    }
}
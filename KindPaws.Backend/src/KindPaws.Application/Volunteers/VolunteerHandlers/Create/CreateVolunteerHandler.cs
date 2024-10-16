using FluentValidation;
using KindPaws.Application.DataBase;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.VolunteerHandlers.Create;

public class CreateVolunteerHandler
{
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVolunteerCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger,
        IValidator<CreateVolunteerCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var emailAddress = EmailAddress.Create(command.EmailAddress).Value;

        var volunteerEmailAddress =
            await _volunteersRepository.GetByEmailAddressAsync(emailAddress, cancellationToken);

        // volunteer email is already exist validation
        if (volunteerEmailAddress.IsSuccess)
            return Errors.General.RecordAlreadyExist(nameof(Volunteer), nameof(EmailAddress)).ToErrorList();

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        // volunteer phone number is already exist validation
        var volunteerPhoneNumber =
            await _volunteersRepository.GetByPhoneNumberAsync(phoneNumber, cancellationToken);

        if (volunteerPhoneNumber.IsSuccess)
            return Errors.General.RecordAlreadyExist(nameof(Volunteer), nameof(PhoneNumber)).ToErrorList();

        var volunteerId = VolunteerId.CreateRandom();

        var fullName = FullName.Create(
            command.FullName.FirstName,
            command.FullName.LastName,
            command.FullName.Patronymic).Value;

        var volunteerToCreate = new Volunteer(
            volunteerId,
            null,
            null,
            fullName,
            emailAddress,
            phoneNumber,
            null,
            null,
            null);

        await _volunteersRepository.AddAsync(volunteerToCreate, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("VOLUNTEER created with ID: {VolunteerId}; " +
                               "Properties: {FullName}, {EmailAddress}, {PhoneNumber}",
            volunteerId.Value,
            fullName,
            emailAddress,
            phoneNumber);

        return volunteerId.Value;
    }
}
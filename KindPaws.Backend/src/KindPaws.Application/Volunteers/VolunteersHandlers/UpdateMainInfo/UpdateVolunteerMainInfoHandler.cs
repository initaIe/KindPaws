using FluentValidation;
using KindPaws.Application.Abstractions.DataBase;
using KindPaws.Application.Extensions;
using KindPaws.Application.Volunteers.VolunteersHandlers.Create;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.VolunteersHandlers.UpdateMainInfo;

public class UpdateVolunteerMainInfoHandler
{
    private readonly IUnitOfWork _dbContext;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IValidator<UpdateVolunteerMainInfoCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public UpdateVolunteerMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger,
        IValidator<UpdateVolunteerMainInfoCommand> validator,
        IUnitOfWork dbContext)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        UpdateVolunteerMainInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(
            volunteerId,
            cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var fullName = FullName.Create(
            command.FullName.FirstName,
            command.FullName.LastName,
            command.FullName.Patronymic).Value;
        var emailAddress = EmailAddress.Create(command.EmailAddress).Value;
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        volunteerResult.Value.UpdateMainInfo(fullName, emailAddress, phoneNumber);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation
        ("VOLUNTEER updated main info with ID: {VolunteerId}; " +
         "Updated properties: {FullName}, {EmailAddress}, {PhoneNumber}",
            volunteerId.Value,
            fullName,
            emailAddress,
            phoneNumber);

        return volunteerId.Value;
    }
}
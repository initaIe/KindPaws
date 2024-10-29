using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateMainInfo;

public class UpdateVolunteerMainInfoHandler
    : ICommandHandler<Guid, UpdateVolunteerMainInfoCommand>
{
    private readonly IEntitiesExistenceValidator<UpdateVolunteerMainInfoExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateVolunteerMainInfoCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public UpdateVolunteerMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger,
        IValidator<UpdateVolunteerMainInfoCommand> validator,
        IUnitOfWork unitOfWork,
        IEntitiesExistenceValidator<UpdateVolunteerMainInfoExistenceValidationData> entitiesExistenceValidator)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _entitiesExistenceValidator = entitiesExistenceValidator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        UpdateVolunteerMainInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        var entitiesExistenceValidationData = command.ToExistenceValidationData();
        var entitiesExistenceValidationResult = await _entitiesExistenceValidator
            .ValidateAsync(entitiesExistenceValidationData, cancellationToken);
        if (entitiesExistenceValidationResult.IsFailure)
            return entitiesExistenceValidationResult.Error.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(
            volunteerId,
            cancellationToken);

        var fullName = FullName.Create(
            command.FullName.FirstName,
            command.FullName.LastName,
            command.FullName.Patronymic).Value;
        var emailAddress = EmailAddress.Create(command.EmailAddress).Value;
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        volunteerResult.Value.UpdateMainInfo(fullName, emailAddress, phoneNumber);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(volunteerId, fullName, emailAddress, phoneNumber);

        return volunteerId.Value;
    }

    private void Log(
        VolunteerId volunteerId,
        FullName fullName,
        EmailAddress emailAddress,
        PhoneNumber phoneNumber)
    {
        _logger.LogInformation
        ("VOLUNTEER updated main info with ID: {Id}; " +
         "Updated properties: {FullName}, {EmailAddress}, {PhoneNumber}",
            volunteerId,
            fullName,
            emailAddress,
            phoneNumber);
    }
}
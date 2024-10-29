using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Application.Helpers;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;

public class CreateVolunteerHandler
    : ICommandHandler<Guid, CreateVolunteerCommand>
{
    private readonly IEntitiesExistenceValidator<CreateVolunteerExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVolunteerCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger,
        IValidator<CreateVolunteerCommand> validator,
        IUnitOfWork unitOfWork,
        IEntitiesExistenceValidator<CreateVolunteerExistenceValidationData> entitiesExistenceValidator)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _entitiesExistenceValidator = entitiesExistenceValidator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        CreateVolunteerCommand command,
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

        var volunteer = VolunteerHelper.ForceCreateNewVolunteer(
            command.FullName,
            command.EmailAddress,
            command.PhoneNumber);

        await _volunteersRepository.AddAsync(volunteer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(volunteer);

        return volunteer.Id.Value;
    }

    private void Log(Volunteer volunteer)
    {
        _logger.LogInformation("VOLUNTEER created with ID: {Id}; " +
                               "Properties: {FullName}, {EmailAddress}, {PhoneNumber}",
            volunteer.Id,
            volunteer.FullName,
            volunteer.EmailAddress,
            volunteer.PhoneNumber);
    }
}
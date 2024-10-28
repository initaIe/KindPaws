using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Application.Helpers;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;

public class CreateVolunteerHandler
    : ICommandHandler<Guid, CreateVolunteerCommand>
{
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVolunteerCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IEntitiesExistenceChecker<CreateVolunteerExistenceCheckData> _entitiesExistenceChecker;

    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger,
        IValidator<CreateVolunteerCommand> validator,
        IUnitOfWork unitOfWork,
        IEntitiesExistenceChecker<CreateVolunteerExistenceCheckData> entitiesExistenceChecker)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _entitiesExistenceChecker = entitiesExistenceChecker;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var existenceCheckData = command.ToExistenceCheckData();
        var existenceCheckerResult = await _entitiesExistenceChecker.CheckAsync(existenceCheckData, cancellationToken);
        if (existenceCheckerResult.IsFailure)
            return existenceCheckerResult.Error.ToErrorList();

        var volunteer = VolunteerHelper.ForceCreateNewVolunteer(
            command.FullName,
            command.EmailAddress,
            command.PhoneNumber);

        await _volunteersRepository.AddAsync(volunteer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("VOLUNTEER created with ID: {VolunteerId}; " +
                               "Properties: {FullName}, {EmailAddress}, {PhoneNumber}",
            volunteer.Id.Value,
            volunteer.FullName,
            volunteer.EmailAddress,
            volunteer.PhoneNumber);

        return volunteer.Id.Value;
    }
}
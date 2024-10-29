using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Delete;

public class DeleteVolunteerHandler
    : ICommandHandler<Guid, DeleteVolunteerCommand>
{
    private readonly IEntitiesExistenceValidator<DeleteVolunteerExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteVolunteerCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<DeleteVolunteerHandler> logger,
        IValidator<DeleteVolunteerCommand> validator,
        IUnitOfWork unitOfWork,
        IEntitiesExistenceValidator<DeleteVolunteerExistenceValidationData> entitiesExistenceValidator)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _entitiesExistenceValidator = entitiesExistenceValidator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        DeleteVolunteerCommand command,
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
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        _volunteersRepository.Delete(volunteerResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(volunteerId);

        return volunteerId.Value;
    }
    
    private void Log(VolunteerId volunteerId)
    {
        _logger.LogInformation(
            "VOLUNTEER soft deleted with ID: {Id}", 
            volunteerId);
    }
}
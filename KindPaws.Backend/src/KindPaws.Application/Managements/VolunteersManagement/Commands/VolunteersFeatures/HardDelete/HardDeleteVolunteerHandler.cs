using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.SoftDelete;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.HardDelete;

// TODO: add delete pets photo minio
public class HardDeleteVolunteerHandler
    : ICommandHandler<Guid, HardDeleteVolunteerCommand>
{
    private readonly IEntitiesExistenceValidator<HardDeleteVolunteerExistenceValidationData>
        _entitiesExistenceValidator;
    private readonly ILogger<HardDeleteVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<HardDeleteVolunteerCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public HardDeleteVolunteerHandler(
        IEntitiesExistenceValidator<HardDeleteVolunteerExistenceValidationData> entitiesExistenceValidator,
        ILogger<HardDeleteVolunteerHandler> logger, 
        IUnitOfWork unitOfWork, 
        IValidator<HardDeleteVolunteerCommand> validator,
        IVolunteersRepository volunteersRepository)
    {
        _entitiesExistenceValidator = entitiesExistenceValidator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        HardDeleteVolunteerCommand command,
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

        _volunteersRepository.HardDelete(volunteerResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(volunteerId);

        return volunteerId.Value;
    }

    private void Log(VolunteerId volunteerId)
    {
        _logger.LogInformation(
            "VOLUNTEER hard deleted with ID: {Id}",
            volunteerId);
    }
}
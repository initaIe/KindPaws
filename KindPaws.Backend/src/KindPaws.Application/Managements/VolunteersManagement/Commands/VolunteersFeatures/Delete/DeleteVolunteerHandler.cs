using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Delete;

public class DeleteVolunteerHandler
    : ICommandHandler<Guid, DeleteVolunteerCommand>
{
    private readonly IEntitiesExistenceChecker<DeleteVolunteerExistenceCheckData> _entitiesExistenceChecker;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteVolunteerCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<DeleteVolunteerHandler> logger,
        IValidator<DeleteVolunteerCommand> validator,
        IUnitOfWork unitOfWork,
        IEntitiesExistenceChecker<DeleteVolunteerExistenceCheckData> entitiesExistenceChecker)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _entitiesExistenceChecker = entitiesExistenceChecker;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        DeleteVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var existenceCheckData = command.ToExistenceCheckData();
        var existenceCheckerResult = await _entitiesExistenceChecker.CheckAsync(existenceCheckData, cancellationToken);
        if (existenceCheckerResult.IsFailure)
            return existenceCheckerResult.Error.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        _volunteersRepository.Delete(volunteerResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("VOLUNTEER soft deleted with ID: {VolunteerId}", volunteerId.Value);

        return volunteerId.Value;
    }
}
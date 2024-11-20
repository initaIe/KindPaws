using FluentValidation;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Abstractions.Validators;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.AggregateRoot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePosition;

public class UpdatePetPositionHandler : ICommandHandler<Guid, UpdatePetPositionCommand>
{
    private readonly IEntitiesExistenceValidator<UpdatePetPositionExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<UpdatePetPositionHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePetPositionCommand> _validator;
    private readonly IRepository<Volunteer, VolunteerId> _volunteersRepository;

    public UpdatePetPositionHandler(
        IEntitiesExistenceValidator<UpdatePetPositionExistenceValidationData> entitiesExistenceValidator,
        ILogger<UpdatePetPositionHandler> logger,
        [FromKeyedServices(Modules.Volunteers)]
        IUnitOfWork unitOfWork,
        IValidator<UpdatePetPositionCommand> validator,
        IRepository<Volunteer, VolunteerId> volunteersRepository)
    {
        _entitiesExistenceValidator = entitiesExistenceValidator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        UpdatePetPositionCommand command,
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

        var petId = PetId.Create(command.PetId).Value;

        var position = Position.Create(command.Position).Value;

        var movePetResult = volunteerResult.Value.MovePet(petId, position);
        if (movePetResult.IsFailure)
            return movePetResult.Error.ToErrorList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(petId, position, volunteerId);

        return petId.Value;
    }

    private void Log(PetId petId, Position newPosition, Guid volunteerId)
    {
        _logger.LogInformation("PET updated position, pet ID: {Id}; " +
                               "Position: {Position}; " +
                               "Owner ID : {VolunteerId}",
            petId,
            newPosition,
            volunteerId);
    }
}
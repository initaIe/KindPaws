using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.SoftDelete;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.HardDelete;

public class HardDeletePetHandler
    : ICommandHandler<Guid, HardDeletePetCommand>
{
    private readonly IEntitiesExistenceValidator<HardDeletePetExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<HardDeletePetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<HardDeletePetCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public HardDeletePetHandler(
        IEntitiesExistenceValidator<HardDeletePetExistenceValidationData> entitiesExistenceValidator,
        ILogger<HardDeletePetHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<HardDeletePetCommand> validator,
        IVolunteersRepository volunteersRepository)
    {
        _entitiesExistenceValidator = entitiesExistenceValidator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        HardDeletePetCommand command,
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

        volunteerResult.Value.HardDeletePet(petId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(petId);

        return Guid.NewGuid();
    }

    private void Log(PetId petId)
    {
        _logger.LogInformation(
            "PET hard deleted with ID: {Id}",
            petId);
    }
}
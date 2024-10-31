using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.SoftDelete;

public class SoftDeletePetHandler
    : ICommandHandler<Guid, SoftDeletePetCommand>
{
    private readonly IEntitiesExistenceValidator<SoftDeletePetExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<SoftDeletePetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SoftDeletePetCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public SoftDeletePetHandler(
        IEntitiesExistenceValidator<SoftDeletePetExistenceValidationData> entitiesExistenceValidator,
        ILogger<SoftDeletePetHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<SoftDeletePetCommand> validator,
        IVolunteersRepository volunteersRepository)
    {
        _entitiesExistenceValidator = entitiesExistenceValidator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        SoftDeletePetCommand command,
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

        volunteerResult.Value.SoftDeletePet(petId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(petId);

        return Guid.NewGuid();
    }

    private void Log(PetId petId)
    {
        _logger.LogInformation(
            "PET soft deleted with ID: {Id}",
            petId);
    }
}
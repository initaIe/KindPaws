using FluentValidation;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Abstractions.Validators;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.AggregateRoot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.HardDelete;

public class HardDeletePetHandler
    : ICommandHandler<Guid, HardDeletePetCommand>
{
    private readonly IEntitiesExistenceValidator<HardDeletePetExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<HardDeletePetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<HardDeletePetCommand> _validator;
    private readonly IRepository<Volunteer, VolunteerId> _volunteersRepository;

    public HardDeletePetHandler(
        IEntitiesExistenceValidator<HardDeletePetExistenceValidationData> entitiesExistenceValidator,
        ILogger<HardDeletePetHandler> logger,
        [FromKeyedServices(Modules.Volunteers)]
        IUnitOfWork unitOfWork,
        IValidator<HardDeletePetCommand> validator,
        IRepository<Volunteer, VolunteerId> volunteersRepository)
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
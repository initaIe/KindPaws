using FluentValidation;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Abstractions.Validators;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateMainInfo;

public class UpdatePetMainInfoHandler
    : ICommandHandler<Guid, UpdatePetMainInfoCommand>
{
    private readonly IEntitiesExistenceValidator<UpdatePetMainInfoExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<UpdatePetMainInfoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePetMainInfoCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public UpdatePetMainInfoHandler(
        ILogger<UpdatePetMainInfoHandler> logger,
        IVolunteersRepository volunteersRepository,
        IValidator<UpdatePetMainInfoCommand> validator,
        [FromKeyedServices(Modules.Volunteers)]
        IUnitOfWork unitOfWork,
        IEntitiesExistenceValidator<UpdatePetMainInfoExistenceValidationData> entitiesExistenceValidator)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _entitiesExistenceValidator = entitiesExistenceValidator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        UpdatePetMainInfoCommand command,
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
        var petResult = volunteerResult.Value.GetPetById(petId);

        var petName = ShortName.Create(command.Name).Value;
        var specieId = SpecieId.Create(command.SpecieId).Value;
        var petType = new PetType(specieId, command.BreedId);

        petResult.Value.UpdateMainInfo(
            petType,
            petName);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(petId, petType, petName, volunteerId);

        return petId.Value;
    }

    private void Log(PetId petId, PetType petType, ShortName petName, VolunteerId volunteerId)
    {
        _logger.LogInformation("PET updated main info with ID: {Id}; " +
                               "Properties: {PetType}, {PetName}; " +
                               "Owner ID : {VolunteerId}",
            petId,
            petType,
            petName,
            volunteerId);
    }
}
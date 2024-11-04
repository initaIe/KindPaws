using FluentValidation;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Helpers;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.Add;

public class AddPetHandler
    : ICommandHandler<Guid, AddPetCommand>
{
    private readonly IEntitiesExistenceValidator<AddPetExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public AddPetHandler(
        ILogger<AddPetHandler> logger,
        IVolunteersRepository volunteersRepository,
        IValidator<AddPetCommand> validator,
        IUnitOfWork unitOfWork,
        IEntitiesExistenceValidator<AddPetExistenceValidationData> entitiesExistenceValidator)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _entitiesExistenceValidator = entitiesExistenceValidator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddPetCommand command,
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

        var pet = PetHelper.ForceCreateNewPet(command.Name, command.SpecieId, command.BreedId);

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        var addPetResult = volunteerResult.Value.AddPet(pet);
        if (addPetResult.IsFailure)
            return addPetResult.Error.ToErrorList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(pet, volunteerId);

        return pet.Id.Value;
    }

    private void Log(Pet pet, VolunteerId volunteerId)
    {
        _logger.LogInformation(
            "PET created with ID: {Id}; " +
            "Properties: {PetType}, {PetName}; " +
            "Owner ID : {VolunteerId}",
            pet.Id,
            pet.PetType,
            pet.Name,
            volunteerId);
    }
}
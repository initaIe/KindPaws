using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Application.Helpers;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.Add;

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

        volunteerResult.Value.AddPet(pet);
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
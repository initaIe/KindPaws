using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateMainInfo;

public class UpdatePetMainInfoHandler
    : ICommandHandler<Guid, UpdatePetMainInfoCommand>
{
    private readonly ILogger<UpdatePetMainInfoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePetMainInfoCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IEntitiesExistenceChecker<UpdatePetMainInfoExistenceCheckData> _entitiesExistenceChecker;

    public UpdatePetMainInfoHandler(
        ILogger<UpdatePetMainInfoHandler> logger,
        IVolunteersRepository volunteersRepository,
        IValidator<UpdatePetMainInfoCommand> validator,
        IUnitOfWork unitOfWork,
        IEntitiesExistenceChecker<UpdatePetMainInfoExistenceCheckData> entitiesExistenceChecker)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _entitiesExistenceChecker = entitiesExistenceChecker;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        UpdatePetMainInfoCommand command,
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

        var petId = PetId.Create(command.PetId).Value;
        var petResult = volunteerResult.Value.GetPetById(petId);

        var petName = ShortName.Create(command.Name).Value;
        var specieId = SpecieId.Create(command.SpecieId).Value;
        var petType = new PetType(specieId, command.BreedId);

        petResult.Value.UpdateMainInfo(
            petType,
            petName);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("PET updated main info with ID: {petId}; " +
                               "Properties: {petType}, {petName}; " +
                               "Owner ID : {volunteerId}",
            petId.Value,
            petType,
            petName,
            volunteerId.Value);

        return petId.Value;
    }
}
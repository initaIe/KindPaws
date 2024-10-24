using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateMainInfo;

public class UpdatePetMainInfoHandler
{
    private readonly ILogger<UpdatePetMainInfoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePetMainInfoCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public UpdatePetMainInfoHandler(
        ILogger<UpdatePetMainInfoHandler> logger,
        IVolunteersRepository volunteersRepository,
        IValidator<UpdatePetMainInfoCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        UpdatePetMainInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petId = PetId.Create(command.PetId).Value;
        var petResult = volunteerResult.Value.GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

        var specieId = SpecieId.Create(command.SpecieId).Value;
        var petType = new PetType(
            specieId, command.BreedId);
        var petName = ShortName.Create(command.Name).Value;

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
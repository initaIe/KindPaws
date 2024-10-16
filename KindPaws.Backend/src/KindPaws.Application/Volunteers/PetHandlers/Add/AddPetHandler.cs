using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.PetHandlers.Add;

public class AddPetHandler
{
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<AddPetCommand> _validator;

    public AddPetHandler(
        ILogger<AddPetHandler> logger,
        IVolunteersRepository volunteersRepository, 
        IValidator<AddPetCommand> validator)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        // TODO: добавить првоерку на то что существует вид и порода
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;

        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petId = PetId.CreateRandom();

        var specieId = SpecieId.Create(command.SpecieId).Value;

        var petType = new PetType(
            specieId, command.BreedId);

        var petName = ShortName.Create(command.Name).Value;

        var pet = new Pet(
            petId,
            petType,
            petName,
            null,
            null,
            null,
            null,
            null,
            null,
            null);

        volunteerResult.Value.AddPet(pet);

        var result = await _volunteersRepository.SaveAsync(volunteerResult.Value, cancellationToken);

        _logger.LogInformation("PET created with ID: {petId}; " +
                               "Properties: {petType}, {petName}; " +
                               "Owner ID : {volunteerId}",
            petId.Value,
            petType,
            petName,
            volunteerId.Value);

        return result;
    }
}
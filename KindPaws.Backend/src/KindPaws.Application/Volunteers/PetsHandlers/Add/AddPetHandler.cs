using FluentValidation;
using KindPaws.Application.DataBase;
using KindPaws.Application.Extensions;
using KindPaws.Application.Species;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.PetsHandlers.Add;

public class AddPetHandler
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public AddPetHandler(
        ILogger<AddPetHandler> logger,
        IVolunteersRepository volunteersRepository,
        IValidator<AddPetCommand> validator,
        IApplicationDbContext dbContext,
        ISpeciesRepository speciesRepository)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _dbContext = dbContext;
        _speciesRepository = speciesRepository;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var specieId = SpecieId.Create(command.SpecieId).Value;
        var specieExistResult = await _speciesRepository.GetByIdAsync(specieId, cancellationToken);
        if (specieExistResult.IsFailure)
            return specieExistResult.Error.ToErrorList();

        var breedExistResult = specieExistResult.Value.GetBreedByGuid(command.BreedId);
        if (breedExistResult.IsFailure)
            return breedExistResult.Error.ToErrorList();

        var petId = PetId.CreateRandom();
        var petType = new PetType(specieId, command.BreedId);
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
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("PET created with ID: {petId}; " +
                               "Properties: {petType}, {petName}; " +
                               "Owner ID : {volunteerId}",
            petId.Value,
            petType,
            petName,
            volunteerId.Value);

        return petId.Value;
    }
}
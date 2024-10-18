using FluentValidation;
using KindPaws.Application.DataBase;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Species.BreedsHandlers.Add;

public class AddBreedHandler
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<AddBreedHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IValidator<AddBreedCommand> _validator;

    public AddBreedHandler(
        IApplicationDbContext dbContext,
        ILogger<AddBreedHandler> logger,
        ISpeciesRepository speciesRepository,
        IValidator<AddBreedCommand> validator)
    {
        _dbContext = dbContext;
        _logger = logger;
        _speciesRepository = speciesRepository;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddBreedCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var specieId = SpecieId.Create(command.SpecieId).Value;
        var specieResult = await _speciesRepository.GetByIdAsync(specieId, cancellationToken);
        if (specieResult.IsFailure)
            return specieResult.Error.ToErrorList();

        var name = ShortName.Create(command.Name).Value;
        var breedExistByName = specieResult.Value.GetBreedByName(name);
        if (breedExistByName.IsSuccess)
            return Errors.General.RecordAlreadyExist(nameof(Breed), nameof(ShortName)).ToErrorList();

        var description = MediumDescription.Create(command.Description).Value;
        var breedId = BreedId.CreateRandom();

        var breed = new Breed(
            breedId,
            name,
            description);

        specieResult.Value.AddBreed(breed);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("BREED added with ID: {breedId}; " +
                               "Properties: {name}, {description}",
            breedId.Value,
            name,
            description);

        return breedId.Value;
    }
}
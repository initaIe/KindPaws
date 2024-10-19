using FluentValidation;
using KindPaws.Application.Abstractions.DataBase;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Species.SpeciesHandlers.Create;

public class CreateSpecieHandler
{
    private readonly IUnitOfWork _dbContext;
    private readonly ILogger<CreateSpecieHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IValidator<CreateSpecieCommand> _validator;

    public CreateSpecieHandler(
        ILogger<CreateSpecieHandler> logger,
        IUnitOfWork dbContext,
        IValidator<CreateSpecieCommand> validator,
        ISpeciesRepository speciesRepository)
    {
        _logger = logger;
        _dbContext = dbContext;
        _validator = validator;
        _speciesRepository = speciesRepository;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        CreateSpecieCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var name = ShortName.Create(command.Name).Value;
        var specieExistByName = await _speciesRepository.GetByName(name, cancellationToken);
        if (specieExistByName.IsSuccess)
            return Errors.General.RecordAlreadyExist(nameof(Specie), nameof(ShortName)).ToErrorList();

        var description = MediumDescription.Create(command.Description).Value;
        var specieId = SpecieId.CreateRandom();

        var specie = new Specie(
            specieId,
            name,
            description);

        await _dbContext.Species.AddAsync(specie, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("SPECIE created with ID: {specieId}; " +
                               "Properties: {name}, {description}",
            specieId.Value,
            name,
            description);

        return specieId.Value;
    }
}
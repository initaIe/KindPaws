using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Species.SpeciesHandlers.Create;

public class CreateSpecieHandler
{
    private readonly ILogger<CreateSpecieHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateSpecieCommand> _validator;

    public CreateSpecieHandler(
        ILogger<CreateSpecieHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<CreateSpecieCommand> validator,
        ISpeciesRepository speciesRepository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
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

        await _speciesRepository.AddAsync(specie, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("SPECIE created with ID: {specieId}; " +
                               "Properties: {name}, {description}",
            specieId.Value,
            name,
            description);

        return specieId.Value;
    }
}
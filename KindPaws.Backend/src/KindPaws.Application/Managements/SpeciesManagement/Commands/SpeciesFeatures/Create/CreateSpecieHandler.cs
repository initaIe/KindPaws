using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Application.Helpers;
using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Create;

public class CreateSpecieHandler
    : ICommandHandler<Guid, CreateSpecieCommand>
{
    private readonly IEntitiesExistenceValidator<CreateSpecieExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<CreateSpecieHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateSpecieCommand> _validator;

    public CreateSpecieHandler(
        ILogger<CreateSpecieHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<CreateSpecieCommand> validator,
        ISpeciesRepository speciesRepository,
        IEntitiesExistenceValidator<CreateSpecieExistenceValidationData> entitiesExistenceValidator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _speciesRepository = speciesRepository;
        _entitiesExistenceValidator = entitiesExistenceValidator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        CreateSpecieCommand command,
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

        var specie = SpecieHelper.ForceCreateNewSpecie(
            command.Name,
            command.Description);

        await _speciesRepository.AddAsync(specie, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(specie);

        return specie.Id.Value;
    }

    private void Log(Specie specie)
    {
        _logger.LogInformation("SPECIE created with ID: {Id}; " +
                               "Properties: {Name}, {Description}",
            specie.Id,
            specie.Name,
            specie.Description);
    }
}
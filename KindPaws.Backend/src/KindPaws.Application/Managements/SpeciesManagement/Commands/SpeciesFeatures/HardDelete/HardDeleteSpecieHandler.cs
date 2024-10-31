using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.HardDelete;

public class HardDeleteSpecieHandler : ICommandHandler<Guid, HardDeleteSpecieCommand>
{
    private readonly IEntitiesExistenceValidator<HardDeleteSpecieExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<HardDeleteSpecieHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<HardDeleteSpecieCommand> _validator;

    public HardDeleteSpecieHandler(
        IEntitiesExistenceValidator<HardDeleteSpecieExistenceValidationData> entitiesExistenceValidator,
        ILogger<HardDeleteSpecieHandler> logger,
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
        IValidator<HardDeleteSpecieCommand> validator)
    {
        _entitiesExistenceValidator = entitiesExistenceValidator;
        _logger = logger;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        HardDeleteSpecieCommand command,
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

        var specieId = SpecieId.Create(command.SpecieId).Value;
        var specie = await _speciesRepository.GetByIdAsync(specieId, cancellationToken);

        _speciesRepository.HardDelete(specie.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(specieId);

        return specieId.Value;
    }

    private void Log(SpecieId specieId)
    {
        _logger.LogInformation(
            "SPECIE hard deleted with ID: {Id}",
            specieId);
    }
}
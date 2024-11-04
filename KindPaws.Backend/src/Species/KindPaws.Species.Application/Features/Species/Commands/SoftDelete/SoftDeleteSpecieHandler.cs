using FluentValidation;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace KindPaws.Species.Application.Features.Species.Commands.SoftDelete;

public class SoftDeleteSpecieHandler : ICommandHandler<Guid, SoftDeleteSpecieCommand>
{
    private readonly IEntitiesExistenceValidator<SoftDeleteSpecieExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<SoftDeleteSpecieHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SoftDeleteSpecieCommand> _validator;

    public SoftDeleteSpecieHandler(
        IEntitiesExistenceValidator<SoftDeleteSpecieExistenceValidationData> entitiesExistenceValidator,
        ILogger<SoftDeleteSpecieHandler> logger,
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
        IValidator<SoftDeleteSpecieCommand> validator)
    {
        _entitiesExistenceValidator = entitiesExistenceValidator;
        _logger = logger;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        SoftDeleteSpecieCommand command,
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
        var specieResult = await _speciesRepository.GetByIdAsync(specieId, cancellationToken);

        specieResult.Value.SoftDelete();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(specieId);

        return specieId.Value;
    }

    private void Log(SpecieId specieId)
    {
        _logger.LogInformation(
            "SPECIE soft deleted with ID: {Id}",
            specieId);
    }
}
using FluentValidation;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Species.Application.Features.Breeds.Commands.SoftDelete;

public class SoftDeleteBreedHandler
    : ICommandHandler<Guid, SoftDeleteBreedCommand>
{
    private readonly IEntitiesExistenceValidator<SoftDeleteBreedExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<SoftDeleteBreedHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SoftDeleteBreedCommand> _validator;

    public SoftDeleteBreedHandler(
        IEntitiesExistenceValidator<SoftDeleteBreedExistenceValidationData> entitiesExistenceValidator,
        ILogger<SoftDeleteBreedHandler> logger,
        ISpeciesRepository speciesRepository,
        [FromKeyedServices(Modules.Species)] IUnitOfWork unitOfWork,
        IValidator<SoftDeleteBreedCommand> validator)
    {
        _entitiesExistenceValidator = entitiesExistenceValidator;
        _logger = logger;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        SoftDeleteBreedCommand command,
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

        var breedId = BreedId.Create(command.BreedId).Value;

        specieResult.Value.SoftDeleteBreed(breedId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(breedId);

        return breedId.Value;
    }

    private void Log(BreedId breedId)
    {
        _logger.LogInformation(
            "BREED soft deleted with ID: {Id}",
            breedId);
    }
}
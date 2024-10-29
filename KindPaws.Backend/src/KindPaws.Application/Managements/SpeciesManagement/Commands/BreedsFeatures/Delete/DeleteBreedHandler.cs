using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Delete;

public class DeleteBreedHandler
    : ICommandHandler<Guid, DeleteBreedCommand>
{
    private readonly IEntitiesExistenceValidator<DeleteBreedExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<DeleteBreedHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteBreedCommand> _validator;

    public DeleteBreedHandler(
        IEntitiesExistenceValidator<DeleteBreedExistenceValidationData> entitiesExistenceValidator, 
        ILogger<DeleteBreedHandler> logger,
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
        IValidator<DeleteBreedCommand> validator)
    {
        _entitiesExistenceValidator = entitiesExistenceValidator;
        _logger = logger;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        DeleteBreedCommand command,
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
        var breed = specieResult.Value.GetBreedById(breedId).Value;
        
        specieResult.Value.DeleteBreed(breed);
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
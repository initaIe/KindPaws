using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Application.Helpers;
using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;

public class AddBreedHandler
    : ICommandHandler<Guid, AddBreedCommand>
{
    private readonly IEntitiesExistenceValidator<AddBreedExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<AddBreedHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddBreedCommand> _validator;

    public AddBreedHandler(
        IUnitOfWork unitOfWork,
        ILogger<AddBreedHandler> logger,
        ISpeciesRepository speciesRepository,
        IValidator<AddBreedCommand> validator,
        IEntitiesExistenceValidator<AddBreedExistenceValidationData> entitiesExistenceValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _speciesRepository = speciesRepository;
        _validator = validator;
        _entitiesExistenceValidator = entitiesExistenceValidator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddBreedCommand command,
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
        
        var breed = BreedHelper.ForceCreateNewBreed(command.Name, command.Description);

        specieResult.Value.AddBreed(breed);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(breed);

        return breed.Id.Value;
    }
    
    private void Log(Breed breed)
    {
        _logger.LogInformation("BREED added with ID: {Id}; " +
                               "Properties: {Name}, {Description}",
            breed.Id,
            breed.Name,
            breed.Description);
    }
}


using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Application.Helpers;
using KindPaws.Domain.Shared.Others;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Create;

public class CreateSpecieHandler
    : ICommandHandler<Guid, CreateSpecieCommand>
{
    private readonly IEntitiesExistenceChecker<CreateSpecieExistenceCheckData> _entitiesExistenceChecker;
    private readonly ILogger<CreateSpecieHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateSpecieCommand> _validator;

    public CreateSpecieHandler(
        ILogger<CreateSpecieHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<CreateSpecieCommand> validator,
        ISpeciesRepository speciesRepository,
        IEntitiesExistenceChecker<CreateSpecieExistenceCheckData> entitiesExistenceChecker)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _speciesRepository = speciesRepository;
        _entitiesExistenceChecker = entitiesExistenceChecker;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        CreateSpecieCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var existenceCheckData = command.ToExistenceCheckData();
        var existenceCheckerResult = await _entitiesExistenceChecker.CheckAsync(existenceCheckData, cancellationToken);
        if (existenceCheckerResult.IsFailure)
            return existenceCheckerResult.Error.ToErrorList();

        var specie = SpecieHelper.ForceCreateNewSpecie(
            command.Name,
            command.Description);

        await _speciesRepository.AddAsync(specie, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("SPECIE created with ID: {specieId}; " +
                               "Properties: {name}, {description}",
            specie.Id.Value,
            specie.Name.Value,
            specie.Description.Value);

        return specie.Id.Value;
    }
}
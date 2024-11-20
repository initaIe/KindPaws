using EntityFramework.Exceptions.Common;
using FluentValidation;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Abstractions.Validators;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Application.Helpers;
using KindPaws.Species.Domain.AggregateRoot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Species.Application.Features.Species.Commands.Create;

public class CreateSpecieHandler
    : ICommandHandler<Guid, CreateSpecieCommand>
{
    private readonly IEntitiesExistenceValidator<CreateSpecieExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<CreateSpecieHandler> _logger;
    private readonly IRepository<Specie, SpecieId> _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateSpecieCommand> _validator;

    public CreateSpecieHandler(
        ILogger<CreateSpecieHandler> logger,
        [FromKeyedServices(Modules.Species)] IUnitOfWork unitOfWork,
        IValidator<CreateSpecieCommand> validator,
        IRepository<Specie, SpecieId> speciesRepository,
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

        try
        {
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
        catch (UniqueConstraintException e) when (e.ConstraintName is "ix_species_name")
        {
            return Errors.General.RecordAlreadyExist(nameof(Specie), nameof(ShortAlphabeticString)).ToErrorList();
        }
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
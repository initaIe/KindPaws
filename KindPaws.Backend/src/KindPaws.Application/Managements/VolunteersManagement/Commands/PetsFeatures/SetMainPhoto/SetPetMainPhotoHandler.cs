using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.SetMainPhoto;

public class SetPetMainPhotoHandler
    : ICommandHandler<Guid, SetPetMainPhotoCommand>
{
    private readonly IEntitiesExistenceValidator<SetPetMainPhotoExistenceValidationData> _entitiesExistenceValidator;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<SetPetMainPhotoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SetPetMainPhotoCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public SetPetMainPhotoHandler(
        IEntitiesExistenceValidator<SetPetMainPhotoExistenceValidationData> entitiesExistenceValidator,
        IFileProvider fileProvider,
        ILogger<SetPetMainPhotoHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<SetPetMainPhotoCommand> validator,
        IVolunteersRepository volunteersRepository)
    {
        _entitiesExistenceValidator = entitiesExistenceValidator;
        _fileProvider = fileProvider;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        SetPetMainPhotoCommand command,
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

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        var petId = PetId.Create(command.PetId).Value;
        var petResult = volunteerResult.Value.GetPetById(petId);

        var filePath = FilePath.Create(command.Path).Value;
        var setMainPhotoResult = petResult.Value.SetMainPhoto(filePath);
        if (setMainPhotoResult.IsFailure)
            return setMainPhotoResult.Error.ToErrorList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(petId, filePath, volunteerId);

        return petId.Value;
    }

    private void Log(PetId petId, FilePath filePath, VolunteerId volunteerId)
    {
        _logger.LogInformation("PET update main photo, pet with ID: {Id}; " +
                               "Main photo path: {FilePath} " +
                               "Owner ID : {VolunteerId}",
            petId,
            filePath,
            volunteerId);
    }
}
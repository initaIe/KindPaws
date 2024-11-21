using FluentValidation;
using KindPaws.Core;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Abstractions.Validators;
using KindPaws.Core.Dtos;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Abstractions;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.DeletePhotos;

public class DeletePetPhotosHandler
    : ICommandHandler<Guid, DeletePetPhotosCommand>
{
    private readonly IEntitiesExistenceValidator<DeletePetPhotosExistenceValidationData> _entitiesExistenceValidator;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<DeletePetPhotosHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeletePetPhotosCommand> _validator;
    private readonly IRepository<Volunteer, VolunteerId> _volunteersRepository;

    public DeletePetPhotosHandler(
        ILogger<DeletePetPhotosHandler> logger,
        IRepository<Volunteer, VolunteerId> volunteersRepository,
        IValidator<DeletePetPhotosCommand> validator,
        IFileProvider fileProvider,
        [FromKeyedServices(Modules.Volunteers)]
        IUnitOfWork unitOfWork,
        IEntitiesExistenceValidator<DeletePetPhotosExistenceValidationData> entitiesExistenceValidator)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _fileProvider = fileProvider;
        _unitOfWork = unitOfWork;
        _entitiesExistenceValidator = entitiesExistenceValidator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        DeletePetPhotosCommand command,
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
        var pet = volunteerResult.Value.GetPetById(petId).Value;

        var photoPathsStringList = command.PhotosPaths.ToList();

        var deleteFilesData = photoPathsStringList.Select(p => new DeleteFileData(
            Constants.FileProvider.PetPhotosBucketName, p));

        List<string> deletedPhotosFilePaths = [];
        foreach (var deleteFileData in deleteFilesData)
        {
            var deletePhotoResult = await _fileProvider.DeleteObjectAsync(deleteFileData, cancellationToken);
            if (deletePhotoResult.IsSuccess)
                deletedPhotosFilePaths.Add(deleteFileData.FileName);
        }

        var filePathsList = deletedPhotosFilePaths.Select(p => FilePath.Create(p).Value).ToList();
        var photos = filePathsList.Select(f => new Photo(f));
        var petPhotos = photos.Select(p => new PetPhoto(p, false));

        volunteerResult.Value.DeletePetPhotos(petId, petPhotos);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(petId, filePathsList, volunteerId);

        return petId.Value;
    }

    private void Log(PetId petId, IEnumerable<FilePath> deletedPhotos, VolunteerId volunteerId)
    {
        _logger.LogInformation("PET deleted photos, pet ID: {Id}; " +
                               "Photo paths: {DeletedPhotosPaths} " +
                               "Owner ID : {VolunteerId}",
            petId,
            deletedPhotos,
            volunteerId);
    }
}
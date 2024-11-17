using FluentValidation;
using KindPaws.Core;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Abstractions.Validators;
using KindPaws.Core.Dtos;
using KindPaws.Core.Extensions;
using KindPaws.Core.Messaging;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.AddPhotos;

public class AddPetPhotosHandler
    : ICommandHandler<Guid, AddPetPhotosCommand>
{
    private readonly IEntitiesExistenceValidator<AddPetPhotosExistenceValidationData> _entitiesExistenceValidator;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<AddPetPhotosHandler> _logger;
    private readonly IMessageQueue<IEnumerable<DeleteFileData>> _messageQueue;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPetPhotosCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public AddPetPhotosHandler(
        ILogger<AddPetPhotosHandler> logger,
        IVolunteersRepository volunteersRepository,
        IValidator<AddPetPhotosCommand> validator,
        IFileProvider fileProvider,
        [FromKeyedServices(Modules.Volunteers)]
        IUnitOfWork unitOfWork,
        IMessageQueue<IEnumerable<DeleteFileData>> messageQueue,
        IEntitiesExistenceValidator<AddPetPhotosExistenceValidationData> entitiesExistenceValidator)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _fileProvider = fileProvider;
        _unitOfWork = unitOfWork;
        _messageQueue = messageQueue;
        _entitiesExistenceValidator = entitiesExistenceValidator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddPetPhotosCommand command,
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

        List<UploadFileData> uploadFilesData = [];
        foreach (var uploadFileDto in command.UploadFileDtos)
        {
            var fileExtension = Path.GetExtension(uploadFileDto.Name);
            var fileName = Guid.NewGuid();
            var filePath = FilePath.Create(fileName, fileExtension);
            if (filePath.IsFailure)
                return filePath.Error.ToErrorList();

            var uploadFileData = new UploadFileData(
                Constants.FileProvider.PetPhotosBucketName,
                filePath.Value,
                uploadFileDto.Stream);
            uploadFilesData.Add(uploadFileData);
        }

        var uploadFilePathsResult = await _fileProvider.UploadObjectsAsync(uploadFilesData, cancellationToken);
        if (uploadFilePathsResult.IsFailure)
        {
            var deleteFilesData
                = uploadFilesData.Select(u => new DeleteFileData(u.BucketName, u.FilePath.Value));
            await _messageQueue.WriteAsync(deleteFilesData, cancellationToken);

            return uploadFilePathsResult.Error;
        }

        var petPhotos = uploadFilePathsResult.Value
            .Select(filePath => new Photo(filePath))
            .Select(photo => new PetPhoto(photo, false));

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        var petId = PetId.Create(command.PetId).Value;
        volunteerResult.Value.AddPetPhotos(petId, petPhotos);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(petId, uploadFilePathsResult.Value, volunteerId);

        return petId.Value;
    }

    private void Log(PetId petId, IEnumerable<FilePath> uploadedFilePaths, VolunteerId volunteerId)
    {
        _logger.LogInformation("PET added photos with ID: {Id}; " +
                               "Photo paths: {PhotoPaths} " +
                               "Owner ID : {VolunteerId}",
            petId,
            uploadedFilePaths,
            volunteerId);
    }
}
using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.DTOs.FileProvider;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.DeletePhotos;

public class DeletePetPhotosHandler
    : ICommandHandler<Guid, DeletePetPhotosCommand>
{
    private const string BucketName = "pet-photos";

    private readonly IEntitiesExistenceValidator<DeletePetPhotosExistenceValidationData> _entitiesExistenceValidator;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<DeletePetPhotosHandler> _logger;
    private readonly IMessageQueue<IEnumerable<DeleteFileData>> _messageQueue;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeletePetPhotosCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public DeletePetPhotosHandler(
        ILogger<DeletePetPhotosHandler> logger,
        IVolunteersRepository volunteersRepository,
        IValidator<DeletePetPhotosCommand> validator,
        IFileProvider fileProvider,
        IUnitOfWork unitOfWork,
        IMessageQueue<IEnumerable<DeleteFileData>> messageQueue,
        IEntitiesExistenceValidator<DeletePetPhotosExistenceValidationData> entitiesExistenceValidator)
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

        List<UploadFileData> uploadFilesData = [];
        foreach (var uploadFileDto in command.UploadFileDtos)
        {
            var fileExtension = Path.GetExtension(uploadFileDto.Name);
            var fileName = Guid.NewGuid();
            var filePath = FilePath.Create(fileName, fileExtension);
            if (filePath.IsFailure)
                return filePath.Error.ToErrorList();

            var uploadFileData = new UploadFileData(BucketName, filePath.Value, uploadFileDto.Stream);
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
        var petResult = volunteerResult.Value.GetPetById(petId);

        petResult.Value.AddPhotos(petPhotos);
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
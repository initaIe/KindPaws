using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.DTOs.FileProvider;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.AddPhotos;

public class AddPetPhotosHandler
    : ICommandHandler<Guid, AddPetPhotosCommand>
{
    private const string BucketName = "pet-photos";

    private readonly IFileProvider _fileProvider;
    private readonly ILogger<AddPetPhotosHandler> _logger;
    private readonly IMessageQueue<IEnumerable<DeleteFileData>> _messageQueue;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPetPhotosCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IEntitiesExistenceChecker<AddPetPhotosExistenceCheckData> _entitiesExistenceChecker;

    public AddPetPhotosHandler(
        ILogger<AddPetPhotosHandler> logger,
        IVolunteersRepository volunteersRepository,
        IValidator<AddPetPhotosCommand> validator,
        IFileProvider fileProvider,
        IUnitOfWork unitOfWork,
        IMessageQueue<IEnumerable<DeleteFileData>> messageQueue,
        IEntitiesExistenceChecker<AddPetPhotosExistenceCheckData> entitiesExistenceChecker)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _fileProvider = fileProvider;
        _unitOfWork = unitOfWork;
        _messageQueue = messageQueue;
        _entitiesExistenceChecker = entitiesExistenceChecker;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddPetPhotosCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var existenceCheckData = command.ToExistenceCheckData();
        var existenceCheckerResult = await _entitiesExistenceChecker.CheckAsync(existenceCheckData, cancellationToken);
        if (existenceCheckerResult.IsFailure)
            return existenceCheckerResult.Error.ToErrorList();

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

        _logger.LogInformation("PET added photos with ID: {petId}; " +
                               "Photo names: {photoName} " +
                               "Owner ID : {volunteerId}",
            petId.Value,
            uploadFilePathsResult.Value.Select(f => f.Value),
            volunteerId.Value);

        return petId.Value;
    }
}
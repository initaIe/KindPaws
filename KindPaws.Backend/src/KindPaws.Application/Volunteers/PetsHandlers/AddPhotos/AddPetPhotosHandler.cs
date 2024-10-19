using FluentValidation;
using KindPaws.Application.Abstractions.DataBase;
using KindPaws.Application.Abstractions.Providers;
using KindPaws.Application.DTOs.FileProvider;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.PetsHandlers.AddPhotos;

public class AddPetPhotosHandler
{
    private const string BucketName = "pet-photos";
    
    private readonly IUnitOfWork _dbContext;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<AddPetPhotosHandler> _logger;
    private readonly IValidator<AddPetPhotosCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public AddPetPhotosHandler(
        ILogger<AddPetPhotosHandler> logger,
        IVolunteersRepository volunteersRepository,
        IValidator<AddPetPhotosCommand> validator,
        IFileProvider fileProvider,
        IUnitOfWork dbContext)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _fileProvider = fileProvider;
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddPetPhotosCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

        try
        {
            var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
            var volunteerExistResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);
            if (volunteerExistResult.IsFailure)
                return volunteerExistResult.Error.ToErrorList();

            var petId = PetId.Create(command.PetId).Value;
            var petResult = volunteerExistResult.Value.GetPetById(petId);
            if (petResult.IsFailure)
                return petResult.Error.ToErrorList();

            List<UploadFileData> uploadFilesData = [];
            foreach (var photoFileDto in command.UploadFileDtos)
            {
                var fileExtension = Path.GetExtension(photoFileDto.Name);
                var fileName = Guid.NewGuid();
                var filePath = FilePath.Create(fileName, fileExtension);
                if (filePath.IsFailure)
                    return filePath.Error.ToErrorList();

                var uploadFileData = new UploadFileData(BucketName, filePath.Value, photoFileDto.Stream);

                uploadFilesData.Add(uploadFileData);
            }

            var uploadFilePathsResult = await _fileProvider.UploadObjectsAsync(uploadFilesData, cancellationToken);
            if (uploadFilePathsResult.IsFailure)
                return uploadFilePathsResult.Error;
            
            var petPhotos = uploadFilePathsResult.Value
                .Select(filePath => new Photo(filePath))
                .Select(photo => new PetPhoto(photo, false));

            petResult.Value.AddPhotos(petPhotos);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return petId.Value;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Can not add photos to pet with ID {petId}",
                command.PetId);

            await transaction.RollbackAsync(cancellationToken);

            return Error.Failure(
                    "volunteer.pet.failure",
                    $"Can not add photos to pet with ID {command.PetId}")
                .ToErrorList();
        }
    }
}
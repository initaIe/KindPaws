using FluentValidation;
using KindPaws.Application.Abstractions;
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
    
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<AddPetPhotosHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPetPhotosCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public AddPetPhotosHandler(
        ILogger<AddPetPhotosHandler> logger,
        IVolunteersRepository volunteersRepository,
        IValidator<AddPetPhotosCommand> validator,
        IFileProvider fileProvider,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _fileProvider = fileProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddPetPhotosCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petId = PetId.Create(command.PetId).Value;
        var petResult = volunteerResult.Value.GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

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
            return uploadFilePathsResult.Error;

        var petPhotos = uploadFilePathsResult.Value
            .Select(filePath => new Photo(filePath))
            .Select(photo => new PetPhoto(photo, false));

        petResult.Value.AddPhotos(petPhotos);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("PET added photos with ID: {petId}; " +
                               "Photo names: {photoName} " +
                               "Owner ID : {volunteerId}",
            petId.Value,
            uploadFilePathsResult.Value.Select(f=>f.Value),
            volunteerId.Value);

        return petId.Value;
    }
}
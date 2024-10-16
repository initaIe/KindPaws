using FluentValidation;
using KindPaws.Application.DataBase;
using KindPaws.Application.Extensions;
using KindPaws.Application.FileProvider;
using KindPaws.Application.Providers;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.PetHandlers.AddPhotos;

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

        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        try
        {
            var volunteerId = VolunteerId.Create(command.VolunteerId).Value;

            var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var petId = PetId.Create(command.PetId).Value;

            var petResult = volunteerResult.Value.GetPetById(petId);

            if (petResult.IsFailure)
                return petResult.Error.ToErrorList();

            List<UploadFileData> uploadFilesData = [];
            foreach (var photoFileDto in command.PhotoFileDtos)
            {
                var extension = Path.GetExtension(photoFileDto.Name);

                var filePath = FilePath.Create(Guid.NewGuid(), extension);
                if (filePath.IsFailure)
                    return filePath.Error.ToErrorList();

                var uploadFileData = new UploadFileData(BucketName, filePath.Value, photoFileDto.Stream);

                uploadFilesData.Add(uploadFileData);
            }

            var petPhotos = uploadFilesData
                .Select(u => u.FilePath)
                .Select(f => new Photo(f))
                .Select(p => new PetPhoto(p, false)); // TODO: is main pet photo

            petResult.Value.AddPhotos(petPhotos);

            await _unitOfWork.SaveChanges(cancellationToken);

            var uploadResult = await _fileProvider.UploadObjectsAsync(uploadFilesData, cancellationToken);

            if (uploadResult.IsFailure)
                return uploadResult.Error;

            transaction.Commit();

            return (Guid)petResult.Value.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Can not add photos to pet with ID {petId}",
                command.PetId);

            transaction.Rollback();

            return Error.Failure(
                    "volunteer.pet.failure",
                    $"Can not add photos to pet with ID {command.PetId}")
                .ToErrorList();
        }
    }
}
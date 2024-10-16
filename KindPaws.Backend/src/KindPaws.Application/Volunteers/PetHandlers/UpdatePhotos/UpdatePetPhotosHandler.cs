using FluentValidation;
using KindPaws.Application.Providers;
using KindPaws.Application.Providers.DTOs;
using KindPaws.Application.Validation;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.PetHandlers.UpdatePhotos;

public class UpdatePetPhotosHandler
{
    private const string BucketName = "pet-photos";
    private readonly ILogger<UpdatePetPhotosHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileProvider _fileProvider;
    private readonly IValidator<UpdatePetPhotosCommand> _validator;

    public UpdatePetPhotosHandler(
        ILogger<UpdatePetPhotosHandler> logger,
        IVolunteersRepository volunteersRepository,
        IValidator<UpdatePetPhotosCommand> validator,
        IFileProvider fileProvider)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _fileProvider = fileProvider;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        UpdatePetPhotosCommand command,
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

        List<FilePath> photoPaths = [];
        List<UploadObjectContent> uploadObjectsContent = [];
        foreach (var photo in command.Photos)
        {
            var extension = Path.GetExtension(photo.Name);
            
            var path = FilePath.Create(Guid.NewGuid().ToString(), extension);
            
            if (path.IsFailure)
                return path.Error.ToErrorList();

            var uploadObjectContent = new UploadObjectContent(
                path.Value.Value,
                photo.Stream);
            
            uploadObjectsContent.Add(uploadObjectContent);
            photoPaths.Add(path.Value);
        }

        var uploadObjectsData = new UploadObjectsData(
            uploadObjectsContent, BucketName);
        
        var uploadResult = await _fileProvider.UploadObjectsAsync(
            uploadObjectsData,
            cancellationToken);
        
        if (uploadResult.IsFailure)
            return uploadResult.Error.ToErrorList();

        var petPhotos = photoPaths.Select(x => new PetPhoto(new Photo(x), false));
        
        petResult.Value.UpdatePhotos(petPhotos);
        
        await _volunteersRepository.SaveAsync(volunteerResult.Value, cancellationToken);

        return volunteerResult.Value.Id.Value;
    }
}
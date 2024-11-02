using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

namespace KindPaws.Application.DTOs;

public record PetPhotoDTO(
    string Path,
    bool IsMain)
{
    public static PetPhotoDTO GetFromDomainModel(PetPhoto petPhoto)
        => new(petPhoto.Photo.FilePath.Value, petPhoto.IsMain);
}
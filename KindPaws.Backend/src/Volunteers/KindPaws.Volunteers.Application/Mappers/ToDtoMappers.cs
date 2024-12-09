using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.Volunteers.Contracts.Dtos;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Application.Mappers;

public static class ToDtoMappers
{
    public static PetPhotoDto ToDto(this PetPhoto petPhoto)
        => new PetPhotoDto
        {
            Path = petPhoto.Photo.FilePath.Value,
            IsMain = petPhoto.IsMain
        };

    public static IReadOnlyList<PetPhotoDto> ToDtoCollection(this IEnumerable<PetPhoto> petPhotos)
        => petPhotos.Select(ToDto).ToList();
}
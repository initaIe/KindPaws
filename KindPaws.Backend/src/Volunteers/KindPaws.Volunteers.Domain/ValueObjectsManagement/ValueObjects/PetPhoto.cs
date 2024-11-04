using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

public record PetPhoto
{
    public PetPhoto(Photo photo, bool isMain)
    {
        Photo = photo;
        IsMain = isMain;
    }

    public Photo Photo { get; }
    public bool IsMain { get; }
}
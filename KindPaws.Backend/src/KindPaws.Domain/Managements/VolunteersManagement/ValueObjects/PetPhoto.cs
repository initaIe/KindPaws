using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

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
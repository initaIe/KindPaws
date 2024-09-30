using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record PetPhoto
{
    private PetPhoto()
    {
    }

    public PetPhoto(Photo photo, bool isMain)
    {
        Photo = photo;
        IsMain = isMain;
    }

    public Photo Photo { get; }
    public bool IsMain { get; }
}
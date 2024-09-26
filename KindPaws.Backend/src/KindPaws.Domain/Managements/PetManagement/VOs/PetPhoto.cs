using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.PetManagement.VOs;

// TODO: add vo photo
public class PetPhoto
{
    private PetPhoto(
        Guid id,
        string path,
        bool isMain)
    {
        Id = id;
        Path = path;
        IsMain = isMain;
    }

    public Guid Id { get; private set; }
    public string Path { get; private set; }
    public bool IsMain { get; private set; }

    public static Result<PetPhoto, IEnumerable<string>> CreateInstanceBinder(
        Guid id,
        string path,
        bool isMain)
    {
        List<string> errors = [];

        id.EmptyValidate()
            .AddErrorIfFailure(errors);

        path.NullOrEmptyOrWhiteSpacesValidate()
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        var petPhoto = new PetPhoto(id, path, isMain);

        return petPhoto;
    }
}
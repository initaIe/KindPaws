using KindPaws.Domain.Managements.PetManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.PetManagement.ValueObjects;

public record PetPhoto
{
    public PetPhoto()
    {
    }

    private PetPhoto(
        string pathToStorage,
        bool isMain)
    {
        PathToStorage = pathToStorage;
        IsMain = isMain;
    }

    public string PathToStorage { get; private set; }
    public bool IsMain { get; private set; }

    public static Result<PetPhoto, IEnumerable<string>> Create(
        string pathToStorage,
        bool isMain)
    {
        List<string> errors = [];

        pathToStorage.DefaultValidate(
                PetPhotoConstraints.MinLength,
                PetPhotoConstraints.MaxLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new PetPhoto(
            pathToStorage,
            isMain);
    }
}
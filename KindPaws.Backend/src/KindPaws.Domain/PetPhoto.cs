using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

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

        id.Validate()
            .AddErrorIfFailure(errors);

        path.NullEmptyWhiteSpacesValidate()
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return Result.Failure<PetPhoto, IEnumerable<string>>(errors);

        var petPhoto = new PetPhoto(id, path, isMain);

        return Result.Success<PetPhoto, IEnumerable<string>>(petPhoto);
    }
}
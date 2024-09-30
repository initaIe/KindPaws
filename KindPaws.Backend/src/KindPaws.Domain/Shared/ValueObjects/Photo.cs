using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects.Constraints;

namespace KindPaws.Domain.Shared.ValueObjects;

public record Photo
{
    private Photo(string pathToStorage)
    {
        PathToStorage = pathToStorage;
    }

    public string PathToStorage { get; }
    
    public static Result<Photo, IEnumerable<string>> Create(string pathToStorage)
    {
        List<string> errors = [];

        pathToStorage.DefaultValidate(
                PhotoConstraints.MinLength,
                PhotoConstraints.MaxLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new Photo(pathToStorage);
    }
}
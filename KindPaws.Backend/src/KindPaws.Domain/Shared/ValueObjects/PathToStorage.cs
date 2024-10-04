using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects.Constraints;

namespace KindPaws.Domain.Shared.ValueObjects;

public record PathToStorage
{
    private PathToStorage(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PathToStorage, Error> Create(string input)
    {
        input = input.Trim();

        if (!StringValidator.IsInRange(input, PathToStorageConstraints.MinLength, PathToStorageConstraints.MaxLength))
            return Errors.General.ValueWrongLength(nameof(input));

        return new PathToStorage(input);
    }
}
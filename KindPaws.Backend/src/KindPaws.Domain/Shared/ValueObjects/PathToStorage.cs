using KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;

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
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(PathToStorage));
        
        input = input.Trim();

        // TODO: Need length constraint?
        if (!StringValidator.IsInRange(input, PathToStorageConstraints.MinLength, PathToStorageConstraints.MaxLength))
            return Errors.General.ValueOutOfRange(nameof(input));

        return new PathToStorage(input);
    }
}
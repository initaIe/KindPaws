using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects.Constraints;

namespace KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;

public record ShortName
{
    private ShortName(string? value)
    {
        Value = value;
    }

    public string? Value { get; }

    public static Result<ShortName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid();

        if (!StringValidator.IsInRange(
                value,
                ShortNameConstraints.MinLength,
                ShortNameConstraints.MaxLength))
            return Errors.General.ValueWrongLength();

        return new ShortName(value);
    }

    public static ShortName CreateEmpty()
    {
        return new ShortName(value: null);
    }
}
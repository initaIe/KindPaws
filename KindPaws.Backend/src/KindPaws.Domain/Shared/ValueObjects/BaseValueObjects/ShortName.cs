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

    public static Result<ShortName, Error> Create(string input)
    {
        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                ShortNameConstraints.MinLength,
                ShortNameConstraints.MaxLength))
            return Errors.General.ValueWrongLength();

        return new ShortName(input);
    }

    public static ShortName CreateEmpty()
    {
        return new ShortName(value: null);
    }
}
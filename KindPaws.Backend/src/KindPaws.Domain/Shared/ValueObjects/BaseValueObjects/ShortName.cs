using KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;

public record ShortName
{
    private ShortName(string value)
    {
        Value = value;
    }

    public string Value { get; }

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
}
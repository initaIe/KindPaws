using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects.Constraints;

namespace KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;

public record MediumDescription
{
    private MediumDescription(string? value)
    {
        Value = value;
    }

    public string? Value { get; }

    public static Result<MediumDescription, Error> Create(string input)
    {
        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                MediumDescriptionConstraints.MinLength,
                MediumDescriptionConstraints.MaxLength))
            return Errors.General.ValueWrongLength();

        return new MediumDescription(input);
    }

    public static MediumDescription CreateEmpty()
    {
        return new MediumDescription(value: null);
    }
}
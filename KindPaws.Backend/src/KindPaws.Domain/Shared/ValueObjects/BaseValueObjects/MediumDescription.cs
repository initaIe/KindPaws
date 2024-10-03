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

    public static Result<MediumDescription, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid();

        if (!StringValidator.IsInRange(
                value,
                MediumDescriptionConstraints.MinLength,
                MediumDescriptionConstraints.MaxLength))
            return Errors.General.ValueWrongLength();

        return new MediumDescription(value);
    }

    public static MediumDescription CreateEmpty()
    {
        return new MediumDescription(value: null);
    }
}
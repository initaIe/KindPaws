using KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;

public record MediumDescription
{
    private MediumDescription(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<MediumDescription, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(ShortName));
        
        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                MediumDescriptionConstraints.MinLength,
                MediumDescriptionConstraints.MaxLength))
            return Errors.General.ValueOutOfRange(nameof(MediumDescription));

        return new MediumDescription(input);
    }
}
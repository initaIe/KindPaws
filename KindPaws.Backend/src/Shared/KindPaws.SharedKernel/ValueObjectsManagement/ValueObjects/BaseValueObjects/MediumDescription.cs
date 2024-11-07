using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;

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
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;

/// <summary>
/// Not nullable, Trim, range.
/// </summary>
public record MediumString
{
    private MediumString(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<MediumString, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(ShortAlphabeticString));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                MediumDescriptionConstraints.MinLength,
                MediumDescriptionConstraints.MaxLength))
            return Errors.General.ValueOutOfRange(nameof(MediumString));

        return new MediumString(input);
    }
}
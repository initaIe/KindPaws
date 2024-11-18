using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;

/// <summary>
/// Not nullable, Trim, ToLower, 1st char to UpperCase, range, alphabetic only.
/// </summary>
public record ShortAlphabeticString
{
    private ShortAlphabeticString(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<ShortAlphabeticString, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(ShortAlphabeticString));

        input = input.Trim().ToLower();
        input = char.ToUpper(input[0]) + input.Substring(1); // TODO

        if (!StringValidator.IsInRange(
                input,
                ShortAlphabeticStringConstraints.MinLength,
                ShortAlphabeticStringConstraints.MaxLength))
            return Errors.General.ValueOutOfRange();

        if (!StringValidator.IsAlphabetic(input))
            return Errors.General.ValueCharacterSetIsInvalid(nameof(ShortAlphabeticString));

        return new ShortAlphabeticString(input);
    }
}
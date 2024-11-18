using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;

/// <summary>
/// Not nullable, Trim, ToLower, 1st char to UpperCase, range, alphabetic and white spaces only.
/// </summary>
public class ShortAlphabeticWhiteSpacesString
{
    private ShortAlphabeticWhiteSpacesString(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<ShortAlphabeticWhiteSpacesString, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(ShortAlphabeticWhiteSpacesString));

        input = input.Trim().ToLower();
        input = char.ToUpper(input[0]) + input.Substring(1);

        if (!StringValidator.IsInRange(
                input,
                ShortAlphabeticWhiteSpacesStringConstraints.MinLength,
                ShortAlphabeticWhiteSpacesStringConstraints.MaxLength))
            return Errors.General.ValueOutOfRange();

        if (!StringValidator.IsAlphabeticWithSpaces(input))
            return Errors.General.ValueCharacterSetIsInvalid(nameof(ShortAlphabeticWhiteSpacesString));

        return new ShortAlphabeticWhiteSpacesString(input);
    }
}
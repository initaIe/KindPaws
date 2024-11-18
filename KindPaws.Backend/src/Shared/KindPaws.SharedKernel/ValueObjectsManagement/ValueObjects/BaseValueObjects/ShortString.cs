using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;

/// <summary>
/// Not nullable, Trim, ToLower, 1st char to UpperCase, range.
/// </summary>
public class ShortString
{
    private ShortString(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<ShortString, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(ShortString));

        input = input.Trim().ToLower();
        input = char.ToUpper(input[0]) + input.Substring(1); // TODO

        if (!StringValidator.IsInRange(
                input,
                ShortStringConstraints.MinLength,
                ShortStringConstraints.MaxLength))
            return Errors.General.ValueOutOfRange();

        return new ShortString(input);
    }
}
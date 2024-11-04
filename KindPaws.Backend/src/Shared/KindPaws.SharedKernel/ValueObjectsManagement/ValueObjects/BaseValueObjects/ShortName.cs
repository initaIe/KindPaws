using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.Utilities.ValidationManagement.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;

public record ShortName
{
    private ShortName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<ShortName, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(ShortName));

        input = input.Trim().ToLower();
        input = char.ToUpper(input[0]) + input.Substring(1); // TODO

        if (!StringValidator.IsInRange(
                input,
                ShortNameConstraints.MinLength,
                ShortNameConstraints.MaxLength))
            return Errors.General.ValueOutOfRange();

        if (!StringValidator.IsAlphabetic(input))
            return Errors.General.ValueCharacterSetIsInvalid(nameof(ShortName));

        return new ShortName(input);
    }
}
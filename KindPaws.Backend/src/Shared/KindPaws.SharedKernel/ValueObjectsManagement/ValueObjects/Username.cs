using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

public record Username
{
    private Username(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Username, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return ErrorsGeneral.ValueIsRequired(nameof(Username));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                UsernameConstraints.MinLength,
                UsernameConstraints.MaxLength))
            return ErrorsGeneral.ValueOutOfRange();

        if (!StringValidator.IsAlphabeticWithDigits(input))
            return ErrorsGeneral.ValueCharacterSetIsInvalid(nameof(Username));

        return new Username(input);
    }
}
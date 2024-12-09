using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

public record UserName
{
    private UserName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<UserName, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return GeneralErrors.ValueIsRequired(nameof(UserName));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                UserNameConstraints.MinLength,
                UserNameConstraints.MaxLength))
            return GeneralErrors.ValueOutOfRange();

        if (!StringValidator.IsAlphabeticWithDigits(input))
            return GeneralErrors.ValueCharacterSetIsInvalid(nameof(UserName));

        return new UserName(input);
    }
}
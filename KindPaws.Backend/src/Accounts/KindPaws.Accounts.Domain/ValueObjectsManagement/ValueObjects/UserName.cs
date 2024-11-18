using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;

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
            return Errors.General.ValueIsRequired(nameof(UserName));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                UserNameConstraints.MinLength,
                UserNameConstraints.MaxLength))
            return Errors.General.ValueOutOfRange();

        if (!StringValidator.IsAlphabeticWithDigits(input))
            return Errors.General.ValueCharacterSetIsInvalid(nameof(UserName));

        return new UserName(input);
    }
}
using KindPaws.Accounts.Domain.Account.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Accounts.Domain.Account.ValueObjectsManagement.ValueObjects;

public record EmailAddress
{
    private EmailAddress(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<EmailAddress, Error> Create(string input)
    {
        if (string.IsNullOrEmpty(input))
            return Errors.General.ValueIsRequired(nameof(input));

        input = input.Trim().ToLower();

        if (!StringValidator.IsInRange(
                input,
                EmailAddressConstraints.MinLength,
                EmailAddressConstraints.MaxLength))
            return Errors.General.ValueOutOfRange(nameof(EmailAddress));

        if (!EmailAddressValidator.Validate(input))
            return Errors.General.ValueFormatIsInvalid(nameof(EmailAddress));

        return new EmailAddress(input);
    }
}
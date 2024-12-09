using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

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
            return ErrorsGeneral.ValueIsRequired(nameof(input));

        input = input.Trim().ToLower();

        if (!StringValidator.IsInRange(
                input,
                EmailAddressConstraints.MinLength,
                EmailAddressConstraints.MaxLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(EmailAddress));

        if (!EmailAddressValidator.Validate(input))
            return ErrorsGeneral.ValueFormatIsInvalid(nameof(EmailAddress));

        return new EmailAddress(input);
    }
}
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.Utilities.ValidationManagement.Validators;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

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
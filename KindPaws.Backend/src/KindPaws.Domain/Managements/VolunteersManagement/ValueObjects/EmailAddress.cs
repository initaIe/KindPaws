using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

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
        
        input = input.Trim();

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
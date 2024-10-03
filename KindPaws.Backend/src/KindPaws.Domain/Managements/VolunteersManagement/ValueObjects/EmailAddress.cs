using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record EmailAddress
{
    private EmailAddress(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<EmailAddress, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid(nameof(EmailAddress));

        if (!StringValidator.IsInRange(
                value,
                EmailAddressConstraints.MinLength,
                EmailAddressConstraints.MaxLength))
            return Errors.General.ValueWrongLength(nameof(EmailAddress));
        
        if (!EmailAddressValidator.Validate(value))
            return Errors.General.ValueIsInvalid(nameof(EmailAddress));

        return new EmailAddress(value);
    }
}
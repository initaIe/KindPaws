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

    public static Result<EmailAddress, Error> Create(string input)
    {
        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                EmailAddressConstraints.MinLength,
                EmailAddressConstraints.MaxLength))
            return Errors.General.ValueWrongLength(nameof(EmailAddress));

        if (!EmailAddressValidator.Validate(input))
            return Errors.General.ValueIsInvalid(nameof(EmailAddress));

        return new EmailAddress(input);
    }
}
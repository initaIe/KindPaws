using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.VolunteerManagement.ValueObjects;

public record EmailAddress
{
    public EmailAddress()
    {
    }

    private EmailAddress(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<EmailAddress, IEnumerable<string>> Create(string value)
    {
        List<string> errors = [];

        value.EmailAddressValidate()
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new EmailAddress(value);
    }
}
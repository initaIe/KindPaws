using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Shared.VOs;

public class EmailAddress
{
    private EmailAddress(string value)
    {
        Value = value;
    }
    public string Value { get; private set; }

    public static Result<EmailAddress, IEnumerable<string>> Create(string value)
    {
        List<string> errors = [];

        value.EmailAddressValidate()
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        var email = new EmailAddress(value);

        return email;
    }
}
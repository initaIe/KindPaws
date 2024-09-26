using System.Text.RegularExpressions;
using KindPaws.Domain.Shared.Others.Validators.ValidatorSettings;

namespace KindPaws.Domain.Shared.Others.Validators;

public static class EmailAddressValidator
{
    public static Result<string> EmailAddressValidate(this string emailAddress)
    {
        var isValid = !string.IsNullOrWhiteSpace(emailAddress)
                      && Regex.IsMatch(emailAddress, EmailSettings.EmailAddressPattern);

        if (isValid)
            return "Email address not valid.";

        return true;
    }
}
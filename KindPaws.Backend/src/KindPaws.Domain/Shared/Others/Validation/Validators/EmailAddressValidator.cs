using System.Text.RegularExpressions;
using KindPaws.Domain.Shared.Others.Validation.ValidatorsAddons;

namespace KindPaws.Domain.Shared.Others.Validation.Validators;

public static class EmailAddressValidator
{
    public static bool Validate(string emailAddress)
    {
        return Regex.IsMatch(emailAddress, EmailAddressAddon.EmailAddressPattern);
    }
}
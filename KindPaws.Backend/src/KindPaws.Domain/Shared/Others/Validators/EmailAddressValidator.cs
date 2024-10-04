using System.Text.RegularExpressions;
using KindPaws.Domain.Shared.Others.Validators.ValidatorAddons;

namespace KindPaws.Domain.Shared.Others.Validators;

public static class EmailAddressValidator
{
    public static bool Validate(string emailAddress)
    {
        return Regex.IsMatch(emailAddress, EmailAddon.EmailAddressPattern);
    }
}
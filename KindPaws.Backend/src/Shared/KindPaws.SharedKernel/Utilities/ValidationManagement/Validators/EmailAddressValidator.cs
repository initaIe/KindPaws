using System.Text.RegularExpressions;
using KindPaws.SharedKernel.Utilities.ValidationManagement.ValidatorsAddons;

namespace KindPaws.SharedKernel.Utilities.ValidationManagement.Validators;

public static class EmailAddressValidator
{
    public static bool Validate(string emailAddress)
    {
        return Regex.IsMatch(emailAddress, EmailAddressAddon.EmailAddressPattern);
    }
}
using System.Text.RegularExpressions;

namespace KindPaws.SharedKernel.Utilities.Validators;

public static class EmailAddressValidator
{
    private const string EmailAddressPattern
        = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
    public static bool Validate(string emailAddress)
    {
        return Regex.IsMatch(emailAddress, EmailAddressPattern);
    }
}
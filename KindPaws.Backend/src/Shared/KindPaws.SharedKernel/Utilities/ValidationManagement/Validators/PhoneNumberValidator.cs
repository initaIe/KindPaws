using System.Text.RegularExpressions;

namespace KindPaws.SharedKernel.Utilities.ValidationManagement.Validators;

public static class PhoneNumberValidator
{
    public static bool Validate(string phoneNumber, string regexPattern)
    {
        return !string.IsNullOrWhiteSpace(phoneNumber)
               && Regex.IsMatch(phoneNumber, regexPattern);
    }
}
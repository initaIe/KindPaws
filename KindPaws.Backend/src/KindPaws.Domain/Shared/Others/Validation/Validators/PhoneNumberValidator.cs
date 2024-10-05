using System.Text.RegularExpressions;

namespace KindPaws.Domain.Shared.Others.Validation.Validators;

public static class PhoneNumberValidator
{
    public static bool Validate(string phoneNumber, string regexPattern)
    {
        return !string.IsNullOrWhiteSpace(phoneNumber)
               && Regex.IsMatch(phoneNumber, regexPattern);
    }
}
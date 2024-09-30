using System.Text.RegularExpressions;

namespace KindPaws.Domain.Shared.Others.Validators;

public static class PhoneNumberValidator
{
    public static Result<string> PhoneNumberValidate(this string phoneNumber, string regexPattern)
    {
        var isValid = !string.IsNullOrWhiteSpace(phoneNumber)
                      && Regex.IsMatch(phoneNumber, regexPattern);

        if (isValid)
            return "Invalid phone number.";

        return true;
    }
}
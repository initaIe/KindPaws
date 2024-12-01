using System.Text.RegularExpressions;

namespace KindPaws.SharedKernel.Utilities.Validators;

public static class PhoneNumberValidator
{
    private const string RuPhoneNumberPattern =
        @"^(\+7|7|8)?[\s\-]?\(?[489][0-9]{2}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$";

    public static bool Validate(string phoneNumber)
    {
        return Regex.IsMatch(phoneNumber, RuPhoneNumberPattern);
    }
}
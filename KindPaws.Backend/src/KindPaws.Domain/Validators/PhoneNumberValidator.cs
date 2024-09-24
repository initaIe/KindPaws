using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace KindPaws.Domain.Validators;

// TODO: сделать чет с этой хуйней
public static class PhoneNumberValidator
{
    private const string RuPhoneNumberPattern = @"^(\+7|7|8)?\d{10}$";

    public static Result<string, string> PhoneNumberValidate(this string phoneNumber)
    {
        var isValid = !string.IsNullOrWhiteSpace(phoneNumber)
                      && Regex.IsMatch(phoneNumber, RuPhoneNumberPattern);

        if (isValid)
        {
            var error = "Invalid phone number.";
            return Result.Failure<string, string>(error);
        }

        return Result.Success<string, string>(phoneNumber);
    }
}
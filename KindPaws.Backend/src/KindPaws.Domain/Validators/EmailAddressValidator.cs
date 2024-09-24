using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace KindPaws.Domain.Validators;

public static class EmailAddressValidator
{
    private const string EmailAddressPattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

    public static Result<string, string> EmailAddressValidate(this string emailAddress)
    {
        var isValid = !string.IsNullOrWhiteSpace(emailAddress)
                      && Regex.IsMatch(emailAddress, EmailAddressPattern);

        if (isValid)
        {
            var error = "Email address not valid.";
            return Result.Failure<string, string>(error);
        }

        return Result.Success<string, string>(emailAddress);
    }
}
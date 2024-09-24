using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class Email
{
    private Email(string emailAddress)
    {
        EmailAddress = emailAddress;
    }

    public string EmailAddress { get; private set; }

    public static Result<Email, IEnumerable<string>> Create(string emailAddress)
    {
        List<string> errors = [];

        emailAddress.EmailAddressValidate()
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return Result.Failure<Email, IEnumerable<string>>(errors);

        var email = new Email(emailAddress);

        return Result.Success<Email, IEnumerable<string>>(email);
    }
}
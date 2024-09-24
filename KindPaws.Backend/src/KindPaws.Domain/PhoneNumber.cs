using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class PhoneNumber
{
    private PhoneNumber(string number)
    {
        Number = number;
    }

    public string Number { get; private set; }

    public static Result<PhoneNumber, IEnumerable<string>> Create(string number)
    {
        List<string> errors = [];

        number.PhoneNumberValidate().AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return Result.Failure<PhoneNumber, IEnumerable<string>>(errors);

        var phoneNumber = new PhoneNumber(number);

        return Result.Success<PhoneNumber, IEnumerable<string>>(phoneNumber);
    }
}
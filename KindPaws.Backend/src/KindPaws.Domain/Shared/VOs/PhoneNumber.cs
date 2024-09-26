using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.Others.Validators.ValidatorSettings;
using KindPaws.Domain.Shared.VOs.Constraints;

namespace KindPaws.Domain.Shared.VOs;

public class PhoneNumber
{
    private PhoneNumber(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static Result<PhoneNumber, IEnumerable<string>> Create(string number)
    {
        List<string> errors = [];

        number.DefaultValidate(
                PhoneNumberConstraints.MinLength, 
                PhoneNumberConstraints.MaxLength)
            .AddErrorIfFailure(errors);

        number.PhoneNumberValidate(PhoneNumberSettings.RuPhoneNumberPattern)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        var phoneNumber = new PhoneNumber(number);

        return phoneNumber;
    }
}
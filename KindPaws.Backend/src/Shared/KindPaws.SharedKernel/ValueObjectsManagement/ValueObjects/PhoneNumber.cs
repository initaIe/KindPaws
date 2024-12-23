using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

public record PhoneNumber
{
    private PhoneNumber(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PhoneNumber, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return ErrorsGeneral.ValueIsRequired(nameof(PhoneNumber));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                PhoneNumberConstraints.MinLength,
                PhoneNumberConstraints.MaxLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(PhoneNumber));

        if (!PhoneNumberValidator.Validate(input))
            return ErrorsGeneral.ValueFormatIsInvalid(nameof(PhoneNumber));

        return new PhoneNumber(input);
    }
}
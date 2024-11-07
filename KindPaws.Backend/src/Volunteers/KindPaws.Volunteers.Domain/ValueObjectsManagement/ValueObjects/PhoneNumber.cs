using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

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
            return Errors.General.ValueIsRequired(nameof(PhoneNumber));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                PhoneNumberConstraints.MinLength,
                PhoneNumberConstraints.MaxLength))
            return Errors.General.ValueOutOfRange(nameof(PhoneNumber));

        if (!PhoneNumberValidator.Validate(input))
            return Errors.General.ValueFormatIsInvalid(nameof(PhoneNumber));

        return new PhoneNumber(input);
    }
}
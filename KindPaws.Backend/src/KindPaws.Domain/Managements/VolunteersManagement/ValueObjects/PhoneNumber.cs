using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;
using KindPaws.Domain.Shared.Others.Validation.ValidatorsAddons;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

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

        if (!PhoneNumberValidator.Validate(input, PhoneNumberAddon.RuPhoneNumberPattern))
            return Errors.General.ValueFormatIsInvalid(nameof(PhoneNumber));

        return new PhoneNumber(input);
    }
}
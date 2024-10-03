using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.Others.Validators.ValidatorAddons;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record PhoneNumber
{
    private PhoneNumber(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PhoneNumber, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid(nameof(PhoneNumber));

        if (!StringValidator.IsInRange(
                value,
                PhoneNumberConstraints.MinLength,
                PhoneNumberConstraints.MaxLength))
            return Errors.General.ValueWrongLength(nameof(PhoneNumber));
        
        if (!PhoneNumberValidator.Validate(value, PhoneNumberAddon.RuPhoneNumberPattern))
            return Errors.General.ValueIsInvalid(nameof(PhoneNumber));

        return new PhoneNumber(value);
    }
}
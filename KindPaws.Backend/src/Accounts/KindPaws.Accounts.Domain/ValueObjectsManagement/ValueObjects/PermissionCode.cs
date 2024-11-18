using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;

public record PermissionCode
{
    private PermissionCode(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PermissionCode, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(PermissionCode));

        input = input.Trim().ToLower();

        if (!StringValidator.IsInRange(
                input,
                ShortStringConstraints.MinLength,
                ShortStringConstraints.MaxLength))
            return Errors.General.ValueOutOfRange();

        if (!StringValidator.IsAlphabeticWithDots(input))
            return Errors.General.ValueCharacterSetIsInvalid(nameof(PermissionCode));

        return new PermissionCode(input);
    }
}
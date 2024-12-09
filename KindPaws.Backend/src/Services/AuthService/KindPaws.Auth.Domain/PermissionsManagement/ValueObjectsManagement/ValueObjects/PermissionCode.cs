using KindPaws.Auth.Domain.PermissionsManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Auth.Domain.PermissionsManagement.ValueObjectsManagement.ValueObjects;

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
            return GeneralErrors.ValueIsRequired(nameof(PermissionCode));

        input = input.Trim().ToLower();

        if (!StringValidator.IsInRange(
                input,
                PermissionCodeConstraints.MinLength,
                PermissionCodeConstraints.MaxLength))
            return GeneralErrors.ValueOutOfRange();

        if (!StringValidator.IsAlphabeticWithDots(input))
            return GeneralErrors.ValueCharacterSetIsInvalid(nameof(PermissionCode));

        return new PermissionCode(input);
    }
}
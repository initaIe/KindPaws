using KindPaws.Roles.Domain.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;

namespace KindPaws.Roles.Domain.ValueObjectsManagement.ValueObjects;

public record RoleName
{
    private RoleName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<RoleName, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(ShortAlphabeticString));

        input = input.Trim().ToProperCase();

        if (!StringValidator.IsInRange(
                input,
                RoleNameConstraints.MinLength,
                RoleNameConstraints.MaxLength))
            return Errors.General.ValueOutOfRange();

        if (!StringValidator.IsAlphabetic(input))
            return Errors.General.ValueCharacterSetIsInvalid(nameof(ShortAlphabeticString));

        return new RoleName(input);
    }
}
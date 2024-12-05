using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Users.Domain.RolesManagement.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Users.Domain.RolesManagement.ValueObjectsManagement.ValueObjects;

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
            return Errors.General.ValueIsRequired(nameof(RoleName));

        input = input.Trim().ToProperCase();

        if (!StringValidator.IsInRange(
                input,
                RoleNameConstraints.MinLength,
                RoleNameConstraints.MaxLength))
            return Errors.General.ValueOutOfRange();

        if (!StringValidator.IsAlphabetic(input))
            return Errors.General.ValueCharacterSetIsInvalid(nameof(RoleName));

        return new RoleName(input);
    }
}
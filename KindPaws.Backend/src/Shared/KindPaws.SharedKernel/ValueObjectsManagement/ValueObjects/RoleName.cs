using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

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
            return GeneralErrors.General.ValueIsRequired(nameof(RoleName));

        input = input.Trim().ToProperCase();

        if (!StringValidator.IsInRange(
                input,
                RoleNameConstraints.MinLength,
                RoleNameConstraints.MaxLength))
            return GeneralErrors.General.ValueOutOfRange();

        if (!StringValidator.IsAlphabetic(input))
            return GeneralErrors.General.ValueCharacterSetIsInvalid(nameof(RoleName));

        return new RoleName(input);
    }
}
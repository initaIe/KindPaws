using KindPaws.Auth.Domain.PermissionsManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;

// TODO: add some validation on upper case/symbols and etc..
public record Password
{
    private Password(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Password, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return ErrorsGeneral.ValueIsRequired(nameof(Password));

        if (!StringValidator.IsInRange(
                input,
                PermissionCodeConstraints.MinLength,
                PermissionCodeConstraints.MaxLength))
            return ErrorsGeneral.ValueOutOfRange();

        return new Password(input);
    }
}
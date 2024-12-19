using KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;

public record PasswordHash
{
    private PasswordHash(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PasswordHash, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return ErrorsGeneral.ValueIsRequired(nameof(PasswordHash));

        if (!StringValidator.IsInRange(
                input,
                PasswordHashConstraints.MinLength,
                PasswordHashConstraints.MaxLength))
            return ErrorsGeneral.ValueOutOfRange();
        
        return new PasswordHash(input);
    }
}
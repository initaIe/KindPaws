using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

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
        // TODO: add validation etc and configure max length ef core
        return new PasswordHash(input);
    }
}
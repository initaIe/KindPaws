using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;

public record PasswordHash
{
    private PasswordHash(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static Result<PasswordHash, Error> Create(string input)
    {
        // TODO: add validation etc
        return new PasswordHash(input);
    }
}
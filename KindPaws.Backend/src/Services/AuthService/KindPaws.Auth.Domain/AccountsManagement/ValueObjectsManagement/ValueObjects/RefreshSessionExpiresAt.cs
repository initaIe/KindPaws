using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;

public class RefreshSessionExpiresAt
{
    private RefreshSessionExpiresAt(DateTimeOffset value)
    {
        Value = value;
    }

    public DateTimeOffset Value { get; }

    public static Result<RefreshSessionExpiresAt, Error> Create(DateTimeOffset input)
    {
        if (input < DateTimeOffset.UtcNow)
            return ErrorsGeneral.ValueIsInvalid(nameof(RefreshSessionExpiresAt));

        return new RefreshSessionExpiresAt(input);
    }
}
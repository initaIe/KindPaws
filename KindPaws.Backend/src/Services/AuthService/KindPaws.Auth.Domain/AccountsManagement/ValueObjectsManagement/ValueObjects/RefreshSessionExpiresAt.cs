using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;

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

    public static Result<RefreshSessionExpiresAt, Error> Create(int expiresInDays)
    {
        if (expiresInDays < 1)
            return ErrorsGeneral.ValueIsInvalid(nameof(RefreshSessionExpiresAt));

        var expiresAtDateTimeOffset = DateTimeOffset.UtcNow.AddDays(expiresInDays);

        return new RefreshSessionExpiresAt(expiresAtDateTimeOffset);
    }
}
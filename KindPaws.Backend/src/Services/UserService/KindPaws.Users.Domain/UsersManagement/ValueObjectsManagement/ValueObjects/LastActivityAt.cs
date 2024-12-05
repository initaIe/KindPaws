using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

public record LastActivityAt
{
    private LastActivityAt(DateTimeOffset value)
    {
        Value = value;
    }

    public DateTimeOffset Value { get; }

    public static Result<LastActivityAt, Error> Create(DateTimeOffset input)
    {
        if (input > DateTimeOffset.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(LastActivityAt));

        return new LastActivityAt(input);
    }
}
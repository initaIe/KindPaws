using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

public record LastModifiedAt
{
    private LastModifiedAt(DateTimeOffset value)
    {
        Value = value;
    }

    public DateTimeOffset Value { get; }

    public static LastModifiedAt CreateNew()
        => new LastModifiedAt(DateTimeOffset.UtcNow);

    public static Result<LastModifiedAt, Error> Create(DateTimeOffset input)
    {
        if (input > DateTimeOffset.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(LastModifiedAt));

        return new LastModifiedAt(input);
    }
}
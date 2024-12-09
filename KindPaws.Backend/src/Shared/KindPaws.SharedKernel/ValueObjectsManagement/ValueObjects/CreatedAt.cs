using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

public record CreatedAt
{
    private CreatedAt(DateTimeOffset value)
    {
        Value = value;
    }

    public DateTimeOffset Value { get; }

    public static CreatedAt CreateNew()
        => new CreatedAt(DateTimeOffset.UtcNow);

    public static Result<CreatedAt, Error> Create(DateTimeOffset input)
    {
        if (input > DateTimeOffset.UtcNow)
            return GeneralErrors.ValueIsInvalid(nameof(CreatedAt));

        return new CreatedAt(input);
    }
}
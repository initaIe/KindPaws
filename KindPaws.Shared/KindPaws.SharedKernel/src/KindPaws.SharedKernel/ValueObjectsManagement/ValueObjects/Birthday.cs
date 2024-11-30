using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

public record Birthday
{
    private Birthday(DateTimeOffset value)
    {
        Value = value;
    }

    public DateTimeOffset Value { get; }

    public static Result<Birthday, Error> Create(DateTimeOffset input)
    {
        if (DateTimeOffsetValidator.IsFromFuture(input))
            return Errors.General.ValueOutOfRange(nameof(Birthday));

        return new Birthday(input);
    }
}
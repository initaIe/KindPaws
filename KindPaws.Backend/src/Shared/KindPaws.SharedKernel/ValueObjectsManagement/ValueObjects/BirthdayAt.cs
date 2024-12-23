using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

public record BirthdayAt
{
    private BirthdayAt(DateTimeOffset value)
    {
        Value = value;
    }

    public DateTimeOffset Value { get; }

    public static Result<BirthdayAt, Error> Create(DateTimeOffset input)
    {
        if (DateTimeOffsetValidator.IsFromFuture(input))
            return ErrorsGeneral.ValueOutOfRange(nameof(BirthdayAt));

        return new BirthdayAt(input);
    }
}
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

public record Birthday
{
    private Birthday(DateTime value)
    {
        Value = value;
    }

    public DateTime Value { get; }

    public static Result<Birthday, Error> Create(DateTime input)
    {
        if (DateTimeValidator.IsFromFuture(input))
            return Errors.General.ValueOutOfRange(nameof(Birthday));

        return new Birthday(input);
    }
}
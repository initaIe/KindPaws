using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

public record Position
{
    public const int ChangeUnit = 1;

    private Position(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Result<Position, Error> Create(int input)
    {
        if (input < 1)
            return Errors.General.ValueOutOfRange(nameof(Position));

        return new Position(input);
    }

    public Result<Position, Error> GetDecreased()
    {
        var decreasedNumber = Value - ChangeUnit;
        var decreasePosition = Create(decreasedNumber);
        if (decreasePosition.IsFailure)
            return decreasePosition.Error;

        return decreasePosition;
    }

    public Result<Position, Error> GetIncreased()
    {
        var increasedNumber = Value + ChangeUnit;
        var increasePosition = Create(increasedNumber);
        if (increasePosition.IsFailure)
            return increasePosition.Error;

        return increasePosition;
    }
}
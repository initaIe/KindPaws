using KindPaws.Domain.Shared.Others;

namespace KindPaws.Domain.Shared.ValueObjects;

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
        if (input < 0)
            return Errors.General.ValueOutOfRange(nameof(Position));

        return new Position(input);
    }
}
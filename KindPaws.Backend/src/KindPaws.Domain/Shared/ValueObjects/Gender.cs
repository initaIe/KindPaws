using KindPaws.Domain.Shared.Others;

namespace KindPaws.Domain.Shared.ValueObjects;

public record Gender
{
    public static readonly Gender Male = new(nameof(Male));
    public static readonly Gender Female = new(nameof(Female));

    private static readonly Gender[] All = [Male, Female];

    private Gender(string? value)
    {
        Value = value;
    }

    public string? Value { get; }

    public static Result<Gender, Error> Create(string value)
    {
        if (All.Any(gender => gender.Value!.ToUpper() == value) == false)
            return Errors.General.ValueIsInvalid(value);

        return new Gender(value);
    }

    public static Gender CreateEmpty()
    {
        return new Gender(value: null);
    }
}
namespace KindPaws.Domain.Shared.ValueObjects;

public record Gender
{
    public static readonly Gender Male = new(nameof(Male));
    public static readonly Gender Female = new(nameof(Female));

    public Gender()
    {
    }

    private Gender(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Gender Create(Gender gender)
    {
        return new Gender(gender.Value);
    }
}
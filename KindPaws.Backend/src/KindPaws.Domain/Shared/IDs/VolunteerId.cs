namespace KindPaws.Domain.Shared.IDs;

public record VolunteerId
{
    private VolunteerId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static VolunteerId Create(Guid value)
    {
        return new VolunteerId(value);
    }

    public static VolunteerId CreateRandom()
    {
        return Create(Guid.NewGuid());
    }

    public static VolunteerId CreateEmpty()
    {
        return Create(Guid.Empty);
    }

    public static implicit operator Guid(VolunteerId volunteerId)
    {
        return volunteerId?.Value ?? throw new ArgumentNullException($"{nameof(volunteerId)} cannot be null.");
    }
}
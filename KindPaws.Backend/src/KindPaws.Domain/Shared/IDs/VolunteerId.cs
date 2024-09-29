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

    public static VolunteerId RandomVolunteerId()
    {
        return Create(Guid.NewGuid());
    }

    public static VolunteerId EmptyVolunteerId()
    {
        return Create(Guid.Empty);
    }
}
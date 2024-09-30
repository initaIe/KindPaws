namespace KindPaws.Domain.Managements.PetManagement.ValueObjects;

public record HealthStatus
{
    public static readonly HealthStatus Critical = new(nameof(Critical));
    public static readonly HealthStatus Weak = new(nameof(Weak));
    public static readonly HealthStatus Stable = new(nameof(Stable));
    public static readonly HealthStatus Healthy = new(nameof(Healthy));
    public static readonly HealthStatus Energetic = new(nameof(Energetic));

    public HealthStatus()
    {
    }

    private HealthStatus(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static HealthStatus Create(HealthStatus healthStatus)
    {
        return new HealthStatus(healthStatus.Value);
    }
}
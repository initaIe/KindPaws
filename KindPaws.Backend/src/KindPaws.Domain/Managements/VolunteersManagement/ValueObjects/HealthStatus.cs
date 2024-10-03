using KindPaws.Domain.Shared.Others;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record HealthStatus
{
    public static readonly HealthStatus Critical = new(nameof(Critical));
    public static readonly HealthStatus Weak = new(nameof(Weak));
    public static readonly HealthStatus Normal = new(nameof(Normal));
    public static readonly HealthStatus Stable = new(nameof(Stable));
    public static readonly HealthStatus Healthy = new(nameof(Healthy));

    private static readonly HealthStatus[] All = [Critical, Weak, Normal, Stable, Healthy];

    private HealthStatus(string? value)
    {
        Value = value;
    }

    public string? Value { get; }

    public static Result<HealthStatus, Error> Create(string? value)
    {
        if (All.Any(healthStatus => healthStatus.Value!.ToUpper() == value) == false)
            return Errors.General.ValueIsInvalid(value);

        return new HealthStatus(value);
    }

    public static HealthStatus CreateEmpty()
    {
        return new HealthStatus(value: null);
    }
}
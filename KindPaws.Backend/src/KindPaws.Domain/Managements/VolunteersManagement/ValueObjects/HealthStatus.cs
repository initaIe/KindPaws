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

    private HealthStatus(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<HealthStatus, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(HealthStatus));

        if (!All.All(healthStatus =>
                string.Equals(healthStatus.Value!, input, StringComparison.CurrentCultureIgnoreCase)))
            return Errors.General.ValueIsInvalid(input);

        return new HealthStatus(input);
    }
}
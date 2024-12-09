using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

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
            return GeneralErrors.ValueIsRequired(nameof(HealthStatus));

        if (!All.Any(h => string.Equals(h.Value, input, StringComparison.CurrentCultureIgnoreCase)))
            return GeneralErrors.ValueIsInvalid(nameof(HealthStatus));

        return new HealthStatus(input);
    }
}
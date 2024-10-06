using KindPaws.Domain.Shared.Constraints.BaseConstraints;

namespace KindPaws.Domain.Managements.VolunteersManagement.Constraints;

public static class HealthStatusConstraints
{
    public const int MinLength = MinLengthConstraints.One;
    public const int MaxLength = MaxLengthConstraints.Medium;
}
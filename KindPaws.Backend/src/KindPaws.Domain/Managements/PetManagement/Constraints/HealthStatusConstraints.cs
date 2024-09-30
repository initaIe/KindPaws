using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Managements.PetManagement.Constraints;

public static class HealthStatusConstraints
{
    public const int MinLength = MinLengthConstraints.One;
    public const int MaxLength = MaxLengthConstraints.Medium;
}
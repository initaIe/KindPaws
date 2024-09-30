using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Managements.VolunteersManagement.Constraints;

public static class HealthDetailsConstraints
{
    public const int MinLength = MinLengthConstraints.One;
    public const int MaxLength = MaxLengthConstraints.VeryLong;
}
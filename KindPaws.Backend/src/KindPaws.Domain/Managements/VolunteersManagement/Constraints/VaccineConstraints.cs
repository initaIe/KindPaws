using KindPaws.Domain.Shared.Constraints.BaseConstraints;

namespace KindPaws.Domain.Managements.VolunteersManagement.Constraints;

public static class VaccineConstraints
{
    public const int MinLength = MinLengthConstraints.One;
    public const int MaxLength = MaxLengthConstraints.Medium;
}
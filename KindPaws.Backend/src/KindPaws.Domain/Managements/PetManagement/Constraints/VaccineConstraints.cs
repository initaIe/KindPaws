using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Managements.PetManagement.Constraints;

public static class VaccineConstraints
{
    public const int MinLength = MinLengthConstraints.One;
    public const int MaxLength = MaxLengthConstraints.Medium;
}
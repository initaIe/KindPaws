using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Managements.SpeciesManagement.Constraints;

public static class BreedColorConstraints
{
    public const int MinLength = MinLengthConstraints.One;
    public const int MaxLength = MaxLengthConstraints.Medium;
}
using KindPaws.Domain.Shared.Constraints.BaseConstraints;

namespace KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;

public static class PathToStorageConstraints
{
    public const int MinLength = LengthConstraints.Min.One;
    public const int MaxLength = LengthConstraints.Max.Extreme;
}
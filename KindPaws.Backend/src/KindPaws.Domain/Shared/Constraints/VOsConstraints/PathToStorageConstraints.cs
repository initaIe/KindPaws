using KindPaws.Domain.Shared.Constraints.BaseConstraints;

namespace KindPaws.Domain.Shared.Constraints.VOsConstraints;

public static class PathToStorageConstraints
{
    public const int MinLength = MinLengthConstraints.One;
    public const int MaxLength = MaxLengthConstraints.Extreme;
}
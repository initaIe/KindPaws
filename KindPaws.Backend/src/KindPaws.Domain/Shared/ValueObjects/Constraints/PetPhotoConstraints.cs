using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Shared.ValueObjects.Constraints;

public static class PhotoConstraints
{
    public const int MinLength = MinLengthConstraints.One;
    public const int MaxLength = MaxLengthConstraints.VeryLong;
}
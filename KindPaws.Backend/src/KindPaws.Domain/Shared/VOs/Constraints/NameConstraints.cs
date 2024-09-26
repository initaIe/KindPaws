using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Shared.VOs.Constraints;

public static class NameConstraints
{
    public const int MinLength = MinLengthConstraints.MinLengthOne;
    public const int MaxLength = MaxLengthConstraints.MaxLengthExtraSmall;
}
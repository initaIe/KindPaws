using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Shared.VOs.Constraints;

public class DescriptionConstraints
{
    public const int MinLength = MinLengthConstraints.MinLengthZero;
    public const int MaxLength = MaxLengthConstraints.MaxLengthExtraLarge;
}
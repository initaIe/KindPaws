using KindPaws.Domain.Shared.Constraints.BaseConstraints;

namespace KindPaws.Domain.Managements.VolunteersManagement.Constraints;

public class SupportStatusConstraints
{
    public const int MinLength = LengthConstraints.Min.One;
    public const int MaxLength = LengthConstraints.Max.Medium;
}
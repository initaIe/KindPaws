using KindPaws.Domain.Shared.Constraints.BaseConstraints;

namespace KindPaws.Domain.Managements.VolunteersManagement.Constraints;

public class RequisiteConstraints
{
    public const int MinNameLength = LengthConstraints.Min.One;
    public const int MaxNameLength = LengthConstraints.Max.Medium;

    public const int MinDescriptionLength = LengthConstraints.Min.One;
    public const int MaxDescriptionLength = LengthConstraints.Max.Long;
}
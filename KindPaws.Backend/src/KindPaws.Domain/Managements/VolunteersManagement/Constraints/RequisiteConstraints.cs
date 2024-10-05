using KindPaws.Domain.Shared.Constraints.BaseConstraints;

namespace KindPaws.Domain.Managements.VolunteersManagement.Constraints;

public class RequisiteConstraints
{
    public const int MinNameLength = MinLengthConstraints.One;
    public const int MaxNameLength = MaxLengthConstraints.Medium;

    public const int MinDescriptionLength = MinLengthConstraints.One;
    public const int MaxDescriptionLength = MaxLengthConstraints.Long;
}
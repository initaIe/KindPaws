using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Managements.PetManagement.Constraints;

public class PetConstraints
{
    public const int MinNameLength = MinLengthConstraints.One;
    public const int MaxNameLength = MaxLengthConstraints.Medium;

    public const int MinDescriptionLength = MinLengthConstraints.One;
    public const int MaxDescriptionLength = MaxLengthConstraints.VeryLong;
}
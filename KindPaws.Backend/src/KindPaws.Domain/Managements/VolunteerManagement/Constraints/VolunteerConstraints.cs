using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Managements.VolunteerManagement.Constraints;

public static class VolunteerConstraints
{
    public const int MinDescriptionLength = MinLengthConstraints.One;
    public const int MaxDescriptionLength = MaxLengthConstraints.VeryLong;

    public const int MinExperienceValue = 0;
}
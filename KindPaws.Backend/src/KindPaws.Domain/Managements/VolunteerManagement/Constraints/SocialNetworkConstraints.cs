using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Managements.VolunteerManagement.Constraints;

public static class SocialNetworkConstraints
{
    public const int MinNameLength = MinLengthConstraints.One;
    public const int MaxNameLength = MaxLengthConstraints.Medium;

    public const int MinLinkLength = MinLengthConstraints.One;
    public const int MaxLinkLength = MaxLengthConstraints.VeryLong;
}
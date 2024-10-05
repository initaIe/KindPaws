using KindPaws.Domain.Shared.Constraints.BaseConstraints;

namespace KindPaws.Domain.Managements.VolunteersManagement.Constraints;

public static class SocialNetworkConstraints
{
    public const int MinNameLength = MinLengthConstraints.One;
    public const int MaxNameLength = MaxLengthConstraints.Medium;

    public const int MinLinkLength = MinLengthConstraints.One;
    public const int MaxLinkLength = MaxLengthConstraints.VeryLong;
}
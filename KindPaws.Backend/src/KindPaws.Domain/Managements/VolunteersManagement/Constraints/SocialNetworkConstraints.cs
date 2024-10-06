using KindPaws.Domain.Shared.Constraints.BaseConstraints;

namespace KindPaws.Domain.Managements.VolunteersManagement.Constraints;

public static class SocialNetworkConstraints
{
    public const int MinNameLength = LengthConstraints.Min.One;
    public const int MaxNameLength = LengthConstraints.Max.Medium;

    public const int MinLinkLength = LengthConstraints.Min.One;
    public const int MaxLinkLength = LengthConstraints.Max.VeryLong;
}
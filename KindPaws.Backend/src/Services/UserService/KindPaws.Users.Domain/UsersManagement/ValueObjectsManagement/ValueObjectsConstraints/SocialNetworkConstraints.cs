using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;

public static class SocialNetworkConstraints
{
    public const int MinNameLength = LengthConstraints.Min.One;
    public const int MaxNameLength = LengthConstraints.Max.Medium;

    public const int MinLinkLength = LengthConstraints.Min.One;
    public const int MaxLinkLength = LengthConstraints.Max.VeryLong;
}
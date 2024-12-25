using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;

public static class ProfileDescriptionConstraints
{
    public const int MinLength = LengthConstraints.Min.Five;
    public const int MaxLength = LengthConstraints.Max.ExtraShort;
}
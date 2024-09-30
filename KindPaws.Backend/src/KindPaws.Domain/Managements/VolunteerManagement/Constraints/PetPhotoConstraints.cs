using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Managements.VolunteerManagement.Constraints;

public static class PetPhotoConstraints
{
    public const int MinLength = MinLengthConstraints.One;
    public const int MaxLength = MaxLengthConstraints.VeryLong;
}
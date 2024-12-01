using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.VolunteerRequests.Domain.ValueObjectsManagement.ValueObjectsConstraints;

public static class RejectionCommentConstraints
{
    public const int MinLength = LengthConstraints.Min.One;
    public const int MaxLength = LengthConstraints.Max.VeryLong;
}
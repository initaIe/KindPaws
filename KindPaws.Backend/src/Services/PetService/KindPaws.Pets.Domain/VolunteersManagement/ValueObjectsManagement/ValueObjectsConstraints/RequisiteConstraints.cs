using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;

public class RequisiteConstraints
{
    public const int MinNameLength = LengthConstraints.Min.One;
    public const int MaxNameLength = LengthConstraints.Max.Medium;

    public const int MinDescriptionLength = LengthConstraints.Min.One;
    public const int MaxDescriptionLength = LengthConstraints.Max.Long;
}
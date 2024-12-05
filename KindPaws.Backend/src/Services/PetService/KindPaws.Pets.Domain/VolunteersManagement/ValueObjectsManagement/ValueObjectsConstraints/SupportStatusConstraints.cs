using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;

public class SupportStatusConstraints
{
    public const int MinLength = LengthConstraints.Min.One;
    public const int MaxLength = LengthConstraints.Max.Medium;
}
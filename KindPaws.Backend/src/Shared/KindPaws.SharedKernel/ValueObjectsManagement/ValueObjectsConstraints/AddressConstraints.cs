using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

public static class AddressConstraints
{
    public const int MinCountryLength = LengthConstraints.Min.One;
    public const int MaxCountryLength = LengthConstraints.Max.Medium;

    public const int MinCityLength = LengthConstraints.Min.One;
    public const int MaxCityLength = LengthConstraints.Max.Medium;

    public const int MinStreetLength = LengthConstraints.Min.One;
    public const int MaxStreetLength = LengthConstraints.Max.Medium;
}
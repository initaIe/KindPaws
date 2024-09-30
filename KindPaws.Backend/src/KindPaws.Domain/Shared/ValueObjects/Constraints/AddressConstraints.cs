using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Shared.ValueObjects.Constraints;

public static class AddressConstraints
{
    public const int MinCountryLength = MinLengthConstraints.One;
    public const int MaxCountryLength = MaxLengthConstraints.Medium;

    public const int MinCityLength = MinLengthConstraints.One;
    public const int MaxCityLength = MaxLengthConstraints.Medium;

    public const int MinStreetLength = MinLengthConstraints.One;
    public const int MaxStreetLength = MaxLengthConstraints.Medium;
}
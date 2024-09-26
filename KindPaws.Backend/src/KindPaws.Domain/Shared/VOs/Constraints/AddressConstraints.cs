using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Shared.VOs.Constraints;

public static class AddressConstraints
{
   public const int MinCountryLength = MinLengthConstraints.MinLengthOne;
   public const int MaxCountryLength = MaxLengthConstraints.MaxLengthSmall;
   
   public const int MinCityLength = MinLengthConstraints.MinLengthOne;
   public const int MaxCityLength = MaxLengthConstraints.MaxLengthSmall;
   
   public const int MinStreetLength = MinLengthConstraints.MinLengthOne;
   public const int MaxStreetLength = MaxLengthConstraints.MaxLengthSmall;
}
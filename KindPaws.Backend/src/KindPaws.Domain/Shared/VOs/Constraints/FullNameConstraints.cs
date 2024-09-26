using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Shared.VOs.Constraints;

public static  class FullNameConstraints
{
    public const int MinFirstNameLength = MinLengthConstraints.MinLengthOne;
    public const int MaxFirstNameLength = MaxLengthConstraints.MaxLengthSmall;
   
    public const int MinLastNameLength = MinLengthConstraints.MinLengthOne;
    public const int MaxLastNameLength = MaxLengthConstraints.MaxLengthSmall;
   
    public const int MinPatronymicLength = MinLengthConstraints.MinLengthOne;
    public const int MaxPatronymicLength = MaxLengthConstraints.MaxLengthSmall;
}
namespace KindPaws.Domain.ValidationRules;

public static class PetHealthRules
{
    public const float MinHeightValue = 0.01f;
    public const int HeightPrecision = 2;
    public const bool IsHeightRoundUp = true;

    public const float MinWeightValue = 0.01f;
    public const int WeightPrecision = 2;
    public const bool IsWeightRoundUp = true;

    public const int MinDescriptionLength = 10;
    public const int MaxDescriptionLength = 250;
}
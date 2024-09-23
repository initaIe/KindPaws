using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

// TODO: add list of diseases and class of disease
// TODO: add class (weight kg etc)/(height cm etc)
public class PetHealth
{
    public const float MinHeightValue = 0.01f;
    public const int HeightPrecision = 2;
    public const bool HeightRoundUp = true;

    public const float MinWeightValue = 0.01f;
    public const int WeightPrecision = 2;
    public const bool WeightRoundUp = true;

    public const int MinDescriptionLength = 10;
    public const int MaxDescriptionLength = 250;

    private PetHealth(
        Guid petId,
        float height,
        float weight,
        bool isNeutered,
        bool isVaccinated,
        string description)
    {
        PetId = petId;
        Height = height;
        Weight = weight;
        IsNeutered = isNeutered;
        IsVaccinated = isVaccinated;
        Description = description;
    }

    public Guid PetId { get; private set; }
    public float Height { get; private set; }
    public float Weight { get; private set; }
    public bool IsNeutered { get; private set; }
    public bool IsVaccinated { get; private set; }
    public string Description { get; private set; }

    public static Result<PetHealth, IEnumerable<string>> Create(
        Guid petId,
        float height,
        float weight,
        bool isNeutered,
        bool isVaccinated,
        string description)
    {
        List<string> errors = [];

        petId.Validate().AddErrorIfFailure(errors);

        height.MinValueValidate(MinHeightValue).AddErrorIfFailure(errors);
        height = height.Round(HeightPrecision, HeightRoundUp);

        weight.MinValueValidate(MinWeightValue).AddErrorIfFailure(errors);
        weight = weight.Round(WeightPrecision, WeightRoundUp);

        description.DefaultValidate(MinDescriptionLength, MaxDescriptionLength);

        if (errors.Count > 0) return Result.Failure<PetHealth, IEnumerable<string>>(errors);

        var petSpecie = new PetHealth(petId, height, weight, isNeutered, isVaccinated, description);

        return Result.Success<PetHealth, IEnumerable<string>>(petSpecie);
    }
}
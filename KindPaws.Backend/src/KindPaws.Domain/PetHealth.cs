using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.ValidationRules;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

// TODO: add list of diseases and class of disease
// TODO: add class (weight kg etc)/(height cm etc)
public class PetHealth
{
    private PetHealth(
        float height,
        float weight,
        bool isNeutered,
        bool isVaccinated,
        string description)
    {
        Height = height;
        Weight = weight;
        IsNeutered = isNeutered;
        IsVaccinated = isVaccinated;
        Description = description;
    }

    public float Height { get; private set; }
    public float Weight { get; private set; }
    public bool IsNeutered { get; private set; }
    public bool IsVaccinated { get; private set; }
    public string Description { get; private set; }

    public static Result<PetHealth, IEnumerable<string>> Create(
        float height,
        float weight,
        bool isNeutered,
        bool isVaccinated,
        string description)
    {
        List<string> errors = [];

        height.MinValueValidate(PetHealthRules.MinHeightValue)
            .AddErrorIfFailure(errors);

        height = height.Round(
            PetHealthRules.HeightPrecision,
            PetHealthRules.IsHeightRoundUp);

        weight.MinValueValidate(PetHealthRules.MinWeightValue)
            .AddErrorIfFailure(errors);

        weight = weight.Round(
            PetHealthRules.WeightPrecision,
            PetHealthRules.IsWeightRoundUp);

        description.DefaultValidate(
                PetHealthRules.MinDescriptionLength,
                PetHealthRules.MaxDescriptionLength)
            .AddErrorsIfFailure(errors);

        if (errors.Count > 0) return Result.Failure<PetHealth, IEnumerable<string>>(errors);

        var petSpecie = new PetHealth(height, weight, isNeutered, isVaccinated, description);

        return Result.Success<PetHealth, IEnumerable<string>>(petSpecie);
    }
}
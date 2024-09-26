using KindPaws.Domain.Managements.PetManagement.AggregateRoot;
using KindPaws.Domain.Managements.PetManagement.Enums;
using KindPaws.Domain.Managements.PetManagement.VOs.ValidationRules;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.PetManagement.VOs;

public record PetCharacteristics
{
    private PetCharacteristics(
        float height, 
        float weight, 
        Gender gender)
    {
        Height = height;
        Weight = weight;
        Gender = gender;
    }

    public float Height { get; private set; }
    public float Weight { get; private set; }
    public Gender Gender { get; private set; }

    public static Result<PetCharacteristics, IEnumerable<string>> Create(
        float height, 
        float weight, 
        Gender gender)
    {
        List<string> errors = [];

        height.MinValueValidate(PetCharacteristicsRules.MinHeightValue)
            .AddErrorIfFailure(errors);

        height = height.Round(
            PetCharacteristicsRules.HeightPrecision,
            PetCharacteristicsRules.IsHeightRoundUp);

        weight.MinValueValidate(PetCharacteristicsRules.MinWeightValue)
            .AddErrorIfFailure(errors);

        weight = weight.Round(
            PetCharacteristicsRules.WeightPrecision,
            PetCharacteristicsRules.IsWeightRoundUp);

        if (errors.Count > 0)
            return errors;

        var petCharacteristics = new PetCharacteristics(
            height,
            weight,
            gender);

        return petCharacteristics;
    }
}
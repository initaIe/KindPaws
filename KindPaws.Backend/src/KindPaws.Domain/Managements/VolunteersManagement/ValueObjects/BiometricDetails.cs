using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record BiometricDetails
{
    private BiometricDetails()
    {
    }

    private BiometricDetails(
        float height,
        float weight,
        Gender gender)
    {
        Height = height;
        Weight = weight;
        Gender = gender;
    }

    public float Height { get; }
    public float Weight { get; }
    public Gender Gender { get; }

    public static Result<BiometricDetails, IEnumerable<string>> Create(
        float height,
        float weight,
        Gender gender)
    {
        List<string> errors = [];

        height.MinValueValidate(BiometricDetailsConstraints.MinHeightValue)
            .AddErrorIfFailure(errors);

        height = height.Round(
            BiometricDetailsConstraints.HeightPrecision,
            BiometricDetailsConstraints.IsHeightRoundUp);

        weight.MinValueValidate(BiometricDetailsConstraints.MinWeightValue)
            .AddErrorIfFailure(errors);

        weight = weight.Round(
            BiometricDetailsConstraints.WeightPrecision,
            BiometricDetailsConstraints.IsWeightRoundUp);

        if (errors.Count > 0)
            return errors;

        return new BiometricDetails(
            height,
            weight,
            gender);
    }
}
using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record BiometricDetails
{
    // ef core
    private BiometricDetails()
    {
    }

    private BiometricDetails(
        float? height,
        float? weight,
        Gender? gender)
    {
        Height = height;
        Weight = weight;
        Gender = gender ?? Gender.CreateEmpty();
    }

    public float? Height { get; }
    public float? Weight { get; }
    public Gender Gender { get; }

    public static Result<BiometricDetails, Error> Create(
        float height,
        float weight,
        Gender gender)
    {
        if (FloatValidator.IsNotLessThan(height, BiometricDetailsConstraints.MinHeightValue))
            return Errors.General.ValueIsInvalid(nameof(height));

        height = height.Round(
            BiometricDetailsConstraints.HeightPrecision,
            BiometricDetailsConstraints.IsHeightRoundUp);

        if (FloatValidator.IsNotLessThan(weight, BiometricDetailsConstraints.MinWeightValue))
            return Errors.General.ValueIsInvalid(nameof(weight));

        weight = weight.Round(
            BiometricDetailsConstraints.WeightPrecision,
            BiometricDetailsConstraints.IsWeightRoundUp);

        return new BiometricDetails(
            height,
            weight,
            gender);
    }

    public static BiometricDetails CreateEmpty()
    {
        return new BiometricDetails(
            null,
            null,
            null);
    }
}
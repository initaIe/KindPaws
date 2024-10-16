using System.Text.Json.Serialization;
using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record Height
{
    [JsonConstructor]
    private Height(float value)
    {
        Value = value;
    }

    public float Value { get; }

    public static Result<Height, Error> Create(float input)
    {
        if (FloatValidator.IsNotLessThan(input, HeightConstraints.MinValue))
            return Errors.General.ValueOutOfRange(nameof(Height));

        input = input.Round(
            HeightConstraints.Precision,
            HeightConstraints.IsRoundUp);

        return new Height(input);
    }
}
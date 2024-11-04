using System.Text.Json.Serialization;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.ValidationManagement.Validators;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

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
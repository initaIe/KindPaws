using System.Text.Json.Serialization;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

public class Weight
{
    [JsonConstructor]
    private Weight(float value)
    {
        Value = value;
    }

    public float Value { get; }

    public static Result<Weight, Error> Create(float input)
    {
        if (FloatValidator.IsNotLessThan(input, WeightConstraints.MinValue))
            return Errors.General.ValueOutOfRange(nameof(Height));

        input = input.Round(
            WeightConstraints.Precision,
            WeightConstraints.IsRoundUp);

        return new Weight(input);
    }
}
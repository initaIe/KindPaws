using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public class Weight
{
    // ef core
    private Weight()
    {
    }

    private Weight(float value)
    {
        Value = value;
    }

    public float? Value { get; }

    public static Result<Weight, Error> Create(float input)
    {
        if (FloatValidator.IsNotLessThan(input, WeightConstraints.MinValue))
            return Errors.General.ValueIsInvalid(nameof(Height));

        input = input.Round(
            WeightConstraints.Precision,
            WeightConstraints.IsRoundUp);

        return new Weight(input);
    }
}
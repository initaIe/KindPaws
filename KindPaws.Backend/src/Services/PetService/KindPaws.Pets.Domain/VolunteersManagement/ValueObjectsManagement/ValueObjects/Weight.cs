using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

public record Weight
{
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
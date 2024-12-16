using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

public record Height
{
    private Height(float value)
    {
        Value = value;
    }

    public float Value { get; }

    public static Result<Height, Error> Create(float input)
    {
        if (FloatValidator.IsNotLessThan(input, HeightConstraints.MinValue))
            return ErrorsGeneral.ValueOutOfRange(nameof(Height));

        input = input.Round(
            HeightConstraints.Precision,
            HeightConstraints.IsRoundUp);

        return new Height(input);
    }
}
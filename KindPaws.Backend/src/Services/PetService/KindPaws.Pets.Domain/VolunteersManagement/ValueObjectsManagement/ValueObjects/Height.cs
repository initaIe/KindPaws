using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

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
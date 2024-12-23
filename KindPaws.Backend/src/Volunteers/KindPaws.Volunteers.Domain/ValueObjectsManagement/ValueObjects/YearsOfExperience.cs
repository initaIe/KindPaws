using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

public record YearsOfExperience
{
    private YearsOfExperience(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Result<YearsOfExperience, Error> Create(int input)
    {
        if (input is < YearsOfExperienceConstraints.MinValue or > YearsOfExperienceConstraints.MaxValue)
            return ErrorsGeneral.ValueOutOfRange(nameof(YearsOfExperience));

        return new YearsOfExperience(input);
    }
}
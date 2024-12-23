using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

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
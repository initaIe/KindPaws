using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

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
            return Errors.General.ValueOutOfRange(nameof(YearsOfExperience));

        return new YearsOfExperience(input);
    }
}
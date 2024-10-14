using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record YearsOfExperience
{
    private YearsOfExperience(int value)
    {
        Value = value;
    }

    public int Value { get; }

    // TODO: добавить валидацию на макс велью
    public static Result<YearsOfExperience, Error> Create(int input)
    {
        if (input < YearsOfExperienceConstraints.MinValue)
            return Errors.General.ValueOutOfRange(nameof(YearsOfExperience));

        return new YearsOfExperience(input);
    }
}
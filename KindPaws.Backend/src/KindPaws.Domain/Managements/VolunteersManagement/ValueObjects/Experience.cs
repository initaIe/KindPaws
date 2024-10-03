using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record Experience
{
    private Experience(int? value)
    {
        Value = value;
    }

    public int? Value { get; }

    public static Result<Experience, Error> Create(int value)
    {
        if (value < ExperienceConstraints.MinExperienceValue)
            return Errors.General.ValueIsInvalid();

        return new Experience(value);
    }

    public static Experience CreateEmpty()
    {
        return new Experience(value: null);
    }
}
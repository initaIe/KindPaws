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

    public static Result<Experience, Error> Create(int input)
    {
        if (input < ExperienceConstraints.MinExperienceValue)
            return Errors.General.ValueIsInvalid();

        return new Experience(input);
    }

    public static Experience CreateEmpty()
    {
        return new Experience(value: null);
    }
}
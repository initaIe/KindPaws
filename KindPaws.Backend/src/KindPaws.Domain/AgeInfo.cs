using CSharpFunctionalExtensions;

namespace KindPaws.Domain;

public class AgeInfo
{
    private AgeInfo(DateOnly birthDate)
    {
        BirthBirthDate = birthDate;
    }

    public DateOnly BirthBirthDate { get; }
    public int YearsOld => CalculateAge();

    private int CalculateAge()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var age = today.Year - BirthBirthDate.Year;
        if (BirthBirthDate > today.AddYears(-age)) age--;
        return age;
    }

    public static Result<AgeInfo, IEnumerable<string>> Create(DateOnly birthDate)
    {
        List<string> errors = [];

        if (birthDate > DateOnly.FromDateTime(DateTime.Now))
            errors.Add("Birth date is earlier than today");

        if (errors.Count > 0)
            return Result.Failure<AgeInfo, IEnumerable<string>>(errors);

        var ageInfo = new AgeInfo(birthDate);

        return Result.Success<AgeInfo, IEnumerable<string>>(ageInfo);
    }
}
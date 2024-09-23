using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class AgeInfo
{
    private AgeInfo(Guid petId, DateOnly birthDate)
    {
        PetId = petId;
        BirthBirthDate = birthDate;
    }

    public Guid PetId { get; private set; }
    public DateOnly BirthBirthDate { get; }
    public int YearsOld => CalculateAge();

    private int CalculateAge()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var age = today.Year - BirthBirthDate.Year;
        if (BirthBirthDate > today.AddYears(-age)) age--;
        return age;
    }

    public static Result<AgeInfo, IEnumerable<string>> Create(Guid petId, DateOnly birthDate)
    {
        List<string> errors = [];

        petId.Validate().AddErrorIfFailure(errors);

        if (birthDate > DateOnly.FromDateTime(DateTime.Now))
            errors.Add("Birth date is earlier than today");

        if (errors.Count > 0) return Result.Failure<AgeInfo, IEnumerable<string>>(errors);

        var ageInfo = new AgeInfo(petId, birthDate);

        return Result.Success<AgeInfo, IEnumerable<string>>(ageInfo);
    }
}
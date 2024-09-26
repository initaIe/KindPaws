using KindPaws.Domain.Managements.PetManagement.Enums;
using KindPaws.Domain.Managements.PetManagement.VOs.ValidationRules;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.PetManagement.VOs;

// TODO: add list of diseases and class of disease
// TODO: add class (weight kg etc)/(height cm etc)
public class PetHealth
{
    private PetHealth(
        bool isNeutered,
        bool isVaccinated,
        string description)
    {
        IsNeutered = isNeutered;
        IsVaccinated = isVaccinated;
        Description = description;
    }
    // TODO: mb add VO PetStateNow(bad, good, in serious condition and etc..)
    
    // TODO: add VO PetCharacteristics and replace height weight gender etc
    public bool IsNeutered { get; private set; }
    public bool IsVaccinated { get; private set; }
    public string Description { get; private set; }

    public static Result<PetHealth, IEnumerable<string>> Create(
        bool isNeutered,
        bool isVaccinated,
        string description)
    {
        List<string> errors = [];

        description.DefaultValidate(
                PetHealthRules.MinDescriptionLength,
                PetHealthRules.MaxDescriptionLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        var petSpecie = new PetHealth(
            isNeutered, 
            isVaccinated, 
            description);

        return petSpecie;
    }
}
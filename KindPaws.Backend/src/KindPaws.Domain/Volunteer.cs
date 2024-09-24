using CSharpFunctionalExtensions;
using KindPaws.Domain.Enums;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.ValidationRules;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class Volunteer
{
    private readonly List<HelpDetail> _helpDetails;
    private readonly List<Pet> _pets;
    private readonly List<SocialNetwork> _socialNetworks;

    private Volunteer(
        List<SocialNetwork> socialNetworks,
        List<HelpDetail> helpDetails,
        List<Pet> pets,
        Guid id,
        FullName fullName,
        Email emailAddress,
        string description,
        int yearsExperience,
        PhoneNumber phoneNumber)
    {
        _socialNetworks = socialNetworks;
        _helpDetails = helpDetails;
        _pets = pets;
        Id = id;
        FullName = fullName;
        EmailAddress = emailAddress;
        Description = description;
        YearsExperience = yearsExperience;
        PhoneNumber = phoneNumber;
    }

    public Guid Id { get; private set; }
    public FullName FullName { get; private set; }
    public Email EmailAddress { get; private set; }
    public string Description { get; private set; }

    // TODO: create Experience class
    public int YearsExperience { get; private set; }
    public int GetCountPetsAlreadyFoundHome => _pets.Count(x => x.HelpInfo.Status == HelpStatus.AlreadyFoundHome);
    public int GetCountPetsLookingHome => _pets.Count(x => x.HelpInfo.Status == HelpStatus.LookingHome);
    public int GetCountPetsNeedHelp => _pets.Count(x => x.HelpInfo.Status == HelpStatus.NeedHelp);
    public PhoneNumber PhoneNumber { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<HelpDetail> HelpDetails => _helpDetails;
    public IReadOnlyList<Pet> Pets => _pets;

    public static Result<Volunteer, IEnumerable<string>> Create(
        List<SocialNetwork> socialNetworks,
        List<HelpDetail> helpDetails,
        List<Pet> pets,
        Guid id,
        FullName fullName,
        Email emailAddress,
        string description,
        int yearsExperience,
        PhoneNumber phoneNumber)
    {
        List<string> errors = [];

        description.DefaultValidate(
                VolunteerRules.MinDescriptionLength,
                VolunteerRules.MaxDescriptionLength)
            .AddErrorsIfFailure(errors);

        if (errors.Count > 0)
            return Result.Failure<Volunteer, IEnumerable<string>>(errors);

        var volunteer = new Volunteer(
            socialNetworks,
            helpDetails,
            pets,
            id,
            fullName,
            emailAddress,
            description,
            yearsExperience,
            phoneNumber);

        return Result.Success<Volunteer, IEnumerable<string>>(volunteer);
    }
}
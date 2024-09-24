using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.ValidationRules;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class Pet
{
    private Pet(
        Guid id,
        string name,
        PetSpecie specie,
        PetHealth health,
        Address address,
        string ownerPhoneNumber,
        AgeInfo ageInfo,
        HelpInfo helpInfo,
        Breed breed,
        BreedColor breedColor)
    {
        Id = id;
        Name = name;
        Specie = specie;
        Health = health;
        Address = address;
        OwnerPhoneNumber = ownerPhoneNumber;
        AgeInfo = ageInfo;
        HelpInfo = helpInfo;
        Breed = breed;
        BreedColor = breedColor;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public PetSpecie Specie { get; private set; }
    public Breed Breed { get; private set; }
    public BreedColor BreedColor { get; private set; }
    public PetHealth Health { get; private set; }
    public Address Address { get; private set; }
    public string OwnerPhoneNumber { get; private set; }
    public AgeInfo AgeInfo { get; private set; }
    public HelpInfo HelpInfo { get; private set; }
    public DateOnly CreationDate { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

    public static Result<Pet, IEnumerable<string>> Create(
        Guid id,
        string name,
        PetSpecie specie,
        PetHealth health,
        Address address,
        string ownerPhoneNumber,
        AgeInfo ageInfo,
        HelpInfo helpInfo,
        Breed breed,
        BreedColor breedColor)
    {
        List<string> errors = [];

        id.Validate()
            .AddErrorIfFailure(errors);
        name.DefaultValidate(PetRules.MinNameLength, PetRules.MaxNameLength)
            .AddErrorsIfFailure(errors);
        ownerPhoneNumber.PhoneNumberValidate()
            .AddErrorIfFailure(errors);

        if (errors.Count > 0) return Result.Failure<Pet, IEnumerable<string>>(errors);

        var pet = new Pet(
            id,
            name,
            specie,
            health,
            address,
            ownerPhoneNumber,
            ageInfo,
            helpInfo,
            breed,
            breedColor);

        return Result.Success<Pet, IEnumerable<string>>(pet);
    }
}
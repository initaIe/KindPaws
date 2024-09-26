using KindPaws.Domain.Managements.PetManagement.VOs;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.VOs;
using KindPaws.Domain.Shared.VOs.Constraints;

namespace KindPaws.Domain.Managements.PetManagement.AggregateRoot;

public class Pet : Entity<PetId>
{
    private readonly List<PetPhoto> _petPhotos;

    private Pet(
        PetId id,
        Name name,
        Description description,
        PetSpecie specie,//
        PetHealth health,//
        Address address,
        PhoneNumber ownerPhoneNumber,
        Age age,
        HelpInfo helpInfo,
        PetBreed petBreed,
        BreedColor breedColor,
        List<PetPhoto> petPhotos) : base(id)
    {
        Name = name;
        Description = description;
        Specie = specie;
        Health = health;
        Address = address;
        OwnerPhoneNumber = ownerPhoneNumber;
        Age = age;
        HelpInfo = helpInfo;
        PetBreed = petBreed;
        BreedColor = breedColor;
        _petPhotos = petPhotos;
    }

    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public PetSpecie Specie { get; private set; }
    public PetBreed PetBreed { get; private set; }
    public BreedColor BreedColor { get; private set; }
    public PetHealth Health { get; private set; }
    public Address Address { get; private set; }
    public PhoneNumber OwnerPhoneNumber { get; private set; }
    public Age Age { get; private set; }
    public HelpInfo HelpInfo { get; private set; }
    public DateOnly CreationDate { get; private set; } = DateOnly.FromDateTime(DateTime.Now);
    public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;

    public static Result<Pet, IEnumerable<string>> Create(
        PetId petId,
        Name name,
        Description description,
        PetSpecie specie,
        PetHealth health,
        Address address,
        PhoneNumber ownerPhoneNumber,
        Age age,
        HelpInfo helpInfo,
        PetBreed petBreed,
        BreedColor breedColor,
        List<PetPhoto> petPhotos)
    {
        var pet = new Pet(
            petId,
            name,
            description,
            specie,
            health,
            address,
            ownerPhoneNumber,
            age,
            helpInfo,
            petBreed,
            breedColor,
            petPhotos);

        return pet;
    }
}
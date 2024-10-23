using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.UnitTests.Shared;

public class Helpers
{
    public static List<Pet> CreatePets(int count)
    {
        return Enumerable.Range(1, count)
            .Select(_ => new Pet(
                PetId.CreateRandom(),
                new PetType(SpecieId.CreateRandom(), Guid.NewGuid()),
                ShortName.Create("test").Value,
                null,
                null,
                null,
                null,
                null,
                null,
                null))
            .ToList();
    }

    public static Pet CreatePet()
    {
        return new Pet(
            PetId.CreateRandom(),
            new PetType(SpecieId.CreateRandom(), Guid.NewGuid()),
            ShortName.Create("test").Value,
            null,
            null,
            null,
            null,
            null,
            null,
            null);
    }

    public static Volunteer CreateVolunteer()
    {
        var volunteerId = VolunteerId.CreateRandom();
        var fullName = FullName.Create("test", "test", "test").Value;
        var emailAddress = EmailAddress.Create("test@test.test").Value;
        var phoneNumber = PhoneNumber.Create("89519533803").Value;

        return new Volunteer(
            volunteerId,
            null,
            null,
            fullName,
            emailAddress,
            phoneNumber,
            null,
            null,
            null);
    }
}
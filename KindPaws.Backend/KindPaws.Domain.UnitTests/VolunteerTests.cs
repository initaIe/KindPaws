using FluentAssertions;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Domain.UnitTests;

public class VolunteerTests
{
    [Fact]
    public void AddPet_FirstAttempt_ShouldReturnSuccessResult()
    {
        // ARRANGE
        var volunteerId = VolunteerId.CreateRandom();
        var fullName = FullName.Create("Sergei", "Bagaev", "Alekseevich").Value;
        var emailAddress = EmailAddress.Create("zxc@zxc.zcx").Value;
        var phoneNumber = PhoneNumber.Create("89519533803").Value;

        var volunteer = new Volunteer(
            volunteerId,
            null,
            null,
            fullName,
            emailAddress,
            phoneNumber,
            null,
            null,
            null);

        var petId = PetId.CreateRandom();
        var specieId = SpecieId.CreateRandom();
        var petType = new PetType(specieId, Guid.NewGuid());
        var petName = ShortName.Create("Bobik").Value;

        var pet = new Pet(
            petId,
            petType,
            petName,
            null,
            null,
            null,
            null,
            null,
            null,
            null);

        // ACT
        var result = volunteer.AddPet(pet);

        // ASSERT
        var addedPetResult = volunteer.GetPetById(petId);
        int firstPetPositionNumber = 1;
        var firstPetPosition = Position.Create(firstPetPositionNumber).Value;

        result.IsSuccess
            .Should()
            .BeTrue();

        result.IsFailure
            .Should()
            .BeFalse();

        addedPetResult.Value.Id
            .Should()
            .Be(petId);

        addedPetResult.Value.Position
            .Should()
            .Be(firstPetPosition);
    }

    [Fact]
    public void AddPet_WithOtherPets_ShouldReturnSuccessResult()
    {
        // ARRANGE
        const int petFirstNumber = 1;
        const int petCount = 5;

        var volunteerId = VolunteerId.CreateRandom();
        var fullName = FullName.Create("Sergei", "Bagaev", "Alekseevich").Value;
        var emailAddress = EmailAddress.Create("zxc@zxc.zcx").Value;
        var phoneNumber = PhoneNumber.Create("89519533803").Value;

        var volunteer = new Volunteer(
            volunteerId,
            null,
            null,
            fullName,
            emailAddress,
            phoneNumber,
            null,
            null,
            null);

        var pets = Enumerable.Range(petFirstNumber, petCount)
            .Select(_ => new Pet(
                PetId.CreateRandom(),
                new PetType(SpecieId.CreateRandom(), Guid.NewGuid()),
                ShortName.Create("Bobik").Value,
                null,
                null,
                null,
                null,
                null,
                null,
                null));

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var petToAddId = PetId.CreateRandom();

        var petToAdd = new Pet(
            petToAddId,
            new PetType(SpecieId.CreateRandom(), Guid.NewGuid()),
            ShortName.Create("Bobik").Value,
            null,
            null,
            null,
            null,
            null,
            null,
            null);

        var petsCount = volunteer.Pets.Count;

        // ACT
        var result = volunteer.AddPet(petToAdd);

        // ASSERT
        var addedPetResult = volunteer.GetPetById(petToAddId);
        var addedPetPositionNumber = petsCount + Position.ChangeUnit;
        var addedPetPosition = Position.Create(addedPetPositionNumber).Value;

        result.IsSuccess
            .Should()
            .BeTrue();

        result.IsFailure
            .Should()
            .BeFalse();

        addedPetResult.Value.Id.Should()
            .Be(petToAddId);

        addedPetResult.Value.Position
            .Should()
            .Be(addedPetPosition);
    }

    [Fact]
    public void DeletePet_WithOtherPets_ShouldReturnSuccessResult()
    {
        // ARRANGE
        const int petCount = 10;
        const int delPetNumber = 5;

        var volunteerId = VolunteerId.CreateRandom();
        var fullName = FullName.Create("Sergei", "Bagaev", "Alekseevich").Value;
        var emailAddress = EmailAddress.Create("zxc@zxc.zcx").Value;
        var phoneNumber = PhoneNumber.Create("89519533803").Value;

        var volunteer = new Volunteer(
            volunteerId,
            null,
            null,
            fullName,
            emailAddress,
            phoneNumber,
            null,
            null,
            null);

        var pets = Enumerable.Range(1, petCount)
            .Select(_ => new Pet(
                PetId.CreateRandom(),
                new PetType(SpecieId.CreateRandom(), Guid.NewGuid()),
                ShortName.Create("Bobik").Value,
                null,
                null,
                null,
                null,
                null,
                null,
                null)).ToList();

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var petIdToDelete = pets.ElementAt(delPetNumber).Id;
        
        var petToDelete = volunteer.GetPetById(petIdToDelete).Value;

        // ACT
        var result = volunteer.DeletePet(petToDelete);

        // ASSERT
        var petExist = volunteer.GetPetById(petIdToDelete);
        var lastPosition = Position.Create(volunteer.Pets[^1].Position.Value);
        
        result.IsSuccess
            .Should()
            .BeTrue();

        result.IsFailure
            .Should()
            .BeFalse();

        petExist.IsFailure
            .Should()
            .BeTrue();
        
        volunteer.Pets[^1].Position
            .Should()
            .Be(lastPosition.Value);
    }
    
    [Fact]
    public void MoveUpPet_WithOtherPets_ShouldReturnSuccessResult()
    {
        // ARRANGE
        const int petCount = 10;
        const int movePetNumber = 2;
        const int moveToNumber = 7;

        var volunteerId = VolunteerId.CreateRandom();
        var fullName = FullName.Create("Sergei", "Bagaev", "Alekseevich").Value;
        var emailAddress = EmailAddress.Create("zxc@zxc.zcx").Value;
        var phoneNumber = PhoneNumber.Create("89519533803").Value;

        var volunteer = new Volunteer(
            volunteerId,
            null,
            null,
            fullName,
            emailAddress,
            phoneNumber,
            null,
            null,
            null);

        var pets = Enumerable.Range(1, petCount)
            .Select(_ => new Pet(
                PetId.CreateRandom(),
                new PetType(SpecieId.CreateRandom(), Guid.NewGuid()),
                ShortName.Create("Bobik").Value,
                null,
                null,
                null,
                null,
                null,
                null,
                null)).ToList();

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var petIdToMove = pets.ElementAt(movePetNumber).Id;
        
        var petToMove = volunteer.GetPetById(petIdToMove).Value;

        var newPositionForMove = Position.Create(moveToNumber).Value;
        

        // ACT
        var result = volunteer.MovePet(petToMove, newPositionForMove);

        // ASSERT
        var positionsAfterMove = volunteer.Pets.Select(p=>p.Position.Value).ToList();
        var mustPositionsAfterMove = Enumerable.Range(1, petCount).ToList();
        
        result.IsSuccess
            .Should()
            .BeTrue();

        result.IsFailure
            .Should()
            .BeFalse();

        positionsAfterMove
            .Should()
            .BeEquivalentTo(mustPositionsAfterMove);
    }
    
    [Fact]
    public void MoveDownPet_WithOtherPets_ShouldReturnSuccessResult()
    {
        // ARRANGE
        const int petCount = 10;
        const int movePetNumber = 7;
        const int moveToNumber = 2;

        var volunteerId = VolunteerId.CreateRandom();
        var fullName = FullName.Create("Sergei", "Bagaev", "Alekseevich").Value;
        var emailAddress = EmailAddress.Create("zxc@zxc.zcx").Value;
        var phoneNumber = PhoneNumber.Create("89519533803").Value;

        var volunteer = new Volunteer(
            volunteerId,
            null,
            null,
            fullName,
            emailAddress,
            phoneNumber,
            null,
            null,
            null);

        var pets = Enumerable.Range(1, petCount)
            .Select(_ => new Pet(
                PetId.CreateRandom(),
                new PetType(SpecieId.CreateRandom(), Guid.NewGuid()),
                ShortName.Create("Bobik").Value,
                null,
                null,
                null,
                null,
                null,
                null,
                null)).ToList();

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var petIdToMove = pets.ElementAt(movePetNumber).Id;
        
        var petToMove = volunteer.GetPetById(petIdToMove).Value;

        var newPositionForMove = Position.Create(moveToNumber).Value;
        

        // ACT
        var result = volunteer.MovePet(petToMove, newPositionForMove);

        // ASSERT
        var positionsAfterMove = volunteer.Pets.Select(p=>p.Position.Value).ToList();
        var mustPositionsAfterMove = Enumerable.Range(1, petCount).ToList();
        
        result.IsSuccess
            .Should()
            .BeTrue();

        result.IsFailure
            .Should()
            .BeFalse();

        positionsAfterMove
            .Should()
            .BeEquivalentTo(mustPositionsAfterMove);
    }
}
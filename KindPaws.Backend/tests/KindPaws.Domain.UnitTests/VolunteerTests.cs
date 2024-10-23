using FluentAssertions;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Domain.UnitTests;

public class VolunteerTests
{
    [Fact]
    public void AddPet_WhenVolunteerHaveNoPets_ShouldSetFirstPositionForAddedPet()
    {
        // ARRANGE
        var volunteer = Helpers.CreateVolunteer();
        var petToAdd = Helpers.CreatePet();
        var petsCountBeforeAddAction = volunteer.Pets.Count;

        // ACT
        var result = volunteer.AddPet(petToAdd);

        // ASSERT
        var getAddedPetResult = volunteer.GetPetById(petToAdd.Id);
        var addedPetShouldHavePositionNumber = petsCountBeforeAddAction + Position.ChangeUnit;
        var addedPetShouldHavePosition = Position.Create(addedPetShouldHavePositionNumber).Value;

        result.IsSuccess
            .Should()
            .BeTrue();

        getAddedPetResult.Value.Id
            .Should()
            .Be(petToAdd.Id);

        getAddedPetResult.Value.Position
            .Should()
            .Be(addedPetShouldHavePosition);
    }

    [Fact]
    public void AddPet_WhenVolunteerHaveFivePets_ShouldSetSixPositionForAddedPet()
    {
        // ARRANGE
        const int petCount = 5;

        var volunteer = Helpers.CreateVolunteer();
        var petsToAdd = Helpers.CreatePets(petCount);

        foreach (var pet in petsToAdd)
            volunteer.AddPet(pet);

        var petToAddAfterOthers = Helpers.CreatePet();
        var petsCountBeforeAddLastPet = volunteer.Pets.Count;

        // ACT
        var result = volunteer.AddPet(petToAddAfterOthers);

        // ASSERT
        var getAddedPetResult = volunteer.GetPetById(petToAddAfterOthers.Id);
        var addedPetShouldHavePositionNumber = petsCountBeforeAddLastPet + Position.ChangeUnit;
        var addedPetShouldHavePosition = Position.Create(addedPetShouldHavePositionNumber).Value;

        result.IsSuccess
            .Should()
            .BeTrue();

        getAddedPetResult.Value.Id.Should()
            .Be(petToAddAfterOthers.Id);

        getAddedPetResult.Value.Position
            .Should()
            .Be(addedPetShouldHavePosition);
    }

    [Fact]
    public void DeletePet_WhenVolunteerHaveTenPets_ShouldDecreasePetsPositionWhoHadLargerPositionThanDeletedPet()
    {
        // ARRANGE
        const int petNumberToDelete = 5;
        const int petCount = 10;

        var volunteer = Helpers.CreateVolunteer();
        var pets = Helpers.CreatePets(petCount);

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var petIdToDelete = pets.ElementAt(petNumberToDelete).Id;
        var petToDelete = volunteer.GetPetById(petIdToDelete).Value;

        // ACT
        var result = volunteer.DeletePet(petToDelete);

        // ASSERT
        var getAddedPetResult = volunteer.GetPetById(petIdToDelete);
        var addedPetShouldHavePositionNumber = volunteer.Pets[^1].Position.Value;
        var addedPetShouldHavePosition = Position.Create(addedPetShouldHavePositionNumber).Value;

        result.IsSuccess
            .Should()
            .BeTrue();

        getAddedPetResult.IsFailure
            .Should()
            .BeTrue();

        volunteer.Pets[^1].Position
            .Should()
            .Be(addedPetShouldHavePosition);
    }

    [Fact]
    public void MovePet_WhenVolunteerHaveTenPets_PetsShouldHavePositionsFromOneToTen()
    {
        // ARRANGE
        const int petCount = 10;

        var volunteer = Helpers.CreateVolunteer();
        var pets = Helpers.CreatePets(petCount);

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        // ACT
        List<Result<Error>> results = [];
        foreach (var petToMove in volunteer.Pets)
        {
            var rnd = new Random();
            var pet = volunteer.GetPetById(petToMove.Id).Value;
            var newRandomPosition = Position.Create(rnd.Next(1, petCount)).Value;
            results.Add(volunteer.MovePet(pet, newRandomPosition));
        }

        // ASSERT
        var positionsAfterMove = volunteer.Pets.Select(p => p.Position.Value).ToList();
        var positionsShouldAfterMove = Enumerable.Range(1, petCount).ToList();
        var shouldResults = Enumerable.Repeat(Result<Error>.Success(), petCount).ToList();

        results.Select(r => r.IsSuccess)
            .Should()
            .BeEquivalentTo(shouldResults.Select(s => s.IsSuccess));

        positionsAfterMove
            .Should()
            .BeEquivalentTo(positionsShouldAfterMove);
    }

    [Fact]
    public void MovePet_WhenVolunteerHavePetsAndMoveablePetIncreasePosition_ShouldMoveCorrectly()
    {
        // ARRANGE
        const int currentPetPositionNumberToMove = 3;
        const int currentPetIndexToMove = currentPetPositionNumberToMove - 1;
        const int targetPetPositionNumberToMove = 7;
        const int petCount = 10;

        var volunteer = Helpers.CreateVolunteer();
        var petsToAdd = Helpers.CreatePets(petCount);

        foreach (var pet in petsToAdd)
            volunteer.AddPet(pet);

        var firstPet = volunteer.Pets.ElementAt(0);
        var secondPet = volunteer.Pets.ElementAt(1);
        var fourthPet = volunteer.Pets.ElementAt(3);
        var fivePet = volunteer.Pets.ElementAt(4);
        var sixPet = volunteer.Pets.ElementAt(5);
        var sevenPet = volunteer.Pets.ElementAt(6);
        var elevenPet = volunteer.Pets.ElementAt(7);
        var ninePet = volunteer.Pets.ElementAt(8);
        var tenPet = volunteer.Pets.ElementAt(9);

        var petToMove = volunteer.Pets.ElementAt(currentPetIndexToMove);
        var positionToMove = Position.Create(targetPetPositionNumberToMove).Value;

        // ACT
        var result = volunteer.MovePet(petToMove, positionToMove);

        // ASSERT
        var getAddedPetResult = volunteer.GetPetById(petToMove.Id);

        result.IsSuccess
            .Should()
            .BeTrue();

        firstPet.Position
            .Should()
            .Be(Position.Create(1).Value);

        secondPet.Position
            .Should()
            .Be(Position.Create(2).Value);

        getAddedPetResult.Value.Position
            .Should()
            .Be(positionToMove); // was 3 pos, should be 7 

        fourthPet.Position
            .Should()
            .Be(Position.Create(3).Value);

        fivePet.Position
            .Should()
            .Be(Position.Create(4).Value);

        sixPet.Position
            .Should()
            .Be(Position.Create(5).Value);

        sevenPet.Position
            .Should()
            .Be(Position.Create(6).Value);

        elevenPet.Position
            .Should()
            .Be(Position.Create(8).Value);

        ninePet.Position
            .Should()
            .Be(Position.Create(9).Value);

        tenPet.Position
            .Should()
            .Be(Position.Create(10).Value);
    }
    
    [Fact]
    public void MovePet_WhenVolunteerHavePetsAndMoveablePetDecreasePosition_ShouldMoveCorrectly()
    {
        // ARRANGE
        const int currentPetPositionNumberToMove = 7;
        const int currentPetIndexToMove = currentPetPositionNumberToMove - 1;
        const int targetPetPositionNumberToMove = 3;
        const int petCount = 10;

        var volunteer = Helpers.CreateVolunteer();
        var petsToAdd = Helpers.CreatePets(petCount);

        foreach (var pet in petsToAdd)
            volunteer.AddPet(pet);

        var firstPet = volunteer.Pets.ElementAt(0);
        var secondPet = volunteer.Pets.ElementAt(1);
        var threePet = volunteer.Pets.ElementAt(2);
        var fourthPet = volunteer.Pets.ElementAt(3);
        var fivePet = volunteer.Pets.ElementAt(4);
        var sixPet = volunteer.Pets.ElementAt(5);
        var eightPet = volunteer.Pets.ElementAt(7);
        var ninePet = volunteer.Pets.ElementAt(8);
        var tenPet = volunteer.Pets.ElementAt(9);

        var petToMove = volunteer.Pets.ElementAt(currentPetIndexToMove);
        var positionToMove = Position.Create(targetPetPositionNumberToMove).Value;

        // ACT
        var result = volunteer.MovePet(petToMove, positionToMove);

        // ASSERT
        var getAddedPetResult = volunteer.GetPetById(petToMove.Id);

        result.IsSuccess
            .Should()
            .BeTrue();

        firstPet.Position
            .Should()
            .Be(Position.Create(1).Value);

        secondPet.Position
            .Should()
            .Be(Position.Create(2).Value);

        getAddedPetResult.Value.Position
            .Should()
            .Be(positionToMove);

        threePet.Position
            .Should()
            .Be(Position.Create(4).Value);

        fourthPet.Position
            .Should()
            .Be(Position.Create(5).Value);

        fivePet.Position
            .Should()
            .Be(Position.Create(6).Value);

        sixPet.Position
            .Should()
            .Be(Position.Create(7).Value);

        eightPet.Position
            .Should()
            .Be(Position.Create(8).Value);

        ninePet.Position
            .Should()
            .Be(Position.Create(9).Value);

        tenPet.Position
            .Should()
            .Be(Position.Create(10).Value);
    }

    [Fact]
    public void MovePet_WhenNewPositionIsCurrentPosition_ShouldNotMove()
    {
        // ARRANGE
        const int currentPetPositionNumberToMove = 7;
        const int currentPetIndexToMove = currentPetPositionNumberToMove - 1;
        const int targetPetPositionNumberToMove = 7;
        const int petCount = 10;

        var volunteer = Helpers.CreateVolunteer();
        var petsToAdd = Helpers.CreatePets(petCount);

        foreach (var pet in petsToAdd)
            volunteer.AddPet(pet);

        var petToMove = volunteer.Pets.ElementAt(currentPetIndexToMove);
        var positionToMove = Position.Create(targetPetPositionNumberToMove).Value;

        // ACT
        var result = volunteer.MovePet(petToMove, positionToMove);

        // ASSERT
        var getAddedPetResult = volunteer.GetPetById(petToMove.Id);

        result.IsSuccess
            .Should()
            .BeTrue();

        getAddedPetResult.Value.Position
            .Should()
            .Be(positionToMove);
    }

    [Fact]
    public void MovePet_WhenVolunteerHaveOnlyOnePet_ShouldNotMove()
    {
        // ARRANGE
        const int petPositionNumberToMove = 5;

        var volunteer = Helpers.CreateVolunteer();
        var pet = Helpers.CreatePet();

        volunteer.AddPet(pet);

        var oldPosition = volunteer.Pets.First().Position;

        var positionToMove = Position.Create(petPositionNumberToMove).Value;

        // ACT
        var result = volunteer.MovePet(pet, positionToMove);

        // ASSERT
        var getAddedPetResult = volunteer.GetPetById(pet.Id);

        result.IsSuccess
            .Should()
            .BeTrue();

        getAddedPetResult.Value.Position
            .Should()
            .Be(oldPosition);
    }

    [Fact]
    public void 
        MovePet_WhenVolunteerHavePetsAndMoveablePetIncreasePositionIsGreaterThanCountOfPets_ShouldMoveToLastPosition()
    {
        // ARRANGE
        const int currentPetPositionNumberToMove = 3;
        const int currentPetIndexToMove = currentPetPositionNumberToMove - 1;
        const int targetPetPositionNumberToMove = 15;
        const int petCount = 10;

        var volunteer = Helpers.CreateVolunteer();
        var petsToAdd = Helpers.CreatePets(petCount);

        foreach (var pet in petsToAdd)
            volunteer.AddPet(pet);

        var firstPet = volunteer.Pets.ElementAt(0);
        var secondPet = volunteer.Pets.ElementAt(1);
        var fourthPet = volunteer.Pets.ElementAt(3);
        var fivePet = volunteer.Pets.ElementAt(4);
        var sixPet = volunteer.Pets.ElementAt(5);
        var sevenPet = volunteer.Pets.ElementAt(6);
        var elevenPet = volunteer.Pets.ElementAt(7);
        var ninePet = volunteer.Pets.ElementAt(8);
        var tenPet = volunteer.Pets.ElementAt(9);

        var petToMove = volunteer.Pets.ElementAt(currentPetIndexToMove);
        var positionToMove = Position.Create(targetPetPositionNumberToMove).Value;
        var shouldPetPosition = Position.Create(petCount).Value;
        

        // ACT
        var result = volunteer.MovePet(petToMove, positionToMove);

        // ASSERT
        var getAddedPetResult = volunteer.GetPetById(petToMove.Id);

        result.IsSuccess
            .Should()
            .BeTrue();

        firstPet.Position
            .Should()
            .Be(Position.Create(1).Value);

        secondPet.Position
            .Should()
            .Be(Position.Create(2).Value);

        getAddedPetResult.Value.Position
            .Should()
            .Be(shouldPetPosition); // was 3 pos, should be 10

        fourthPet.Position
            .Should()
            .Be(Position.Create(3).Value);

        fivePet.Position
            .Should()
            .Be(Position.Create(4).Value);

        sixPet.Position
            .Should()
            .Be(Position.Create(5).Value);

        sevenPet.Position
            .Should()
            .Be(Position.Create(6).Value);

        elevenPet.Position
            .Should()
            .Be(Position.Create(7).Value);

        ninePet.Position
            .Should()
            .Be(Position.Create(8).Value);

        tenPet.Position
            .Should()
            .Be(Position.Create(9).Value);
    }
}
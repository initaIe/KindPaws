using FluentAssertions;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.UnitTests.Shared;

namespace KindPaws.Domain.UnitTests.Volunteer.PositionFeature;

public class AddPetTests
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
}
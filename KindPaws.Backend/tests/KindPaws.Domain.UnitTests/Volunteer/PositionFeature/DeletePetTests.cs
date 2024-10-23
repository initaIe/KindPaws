using FluentAssertions;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.UnitTests.Shared;

namespace KindPaws.Domain.UnitTests.Volunteer.PositionFeature;

public class DeletePetTests
{
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
}
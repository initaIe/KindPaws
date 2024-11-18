using KindPaws.Core.Dtos;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Application.Helpers;

public static class VolunteerHelper
{
    public static Volunteer ForceCreateNewVolunteer(
        string? description,
        AddressDto? address,
        int? yearsOfExperience,
        IEnumerable<RequisiteDto> requisites)
    {
        var volunteerId = VolunteerId.CreateRandom();
        var volunteerDescription = MediumString.Create(description!).Value;
        var volunteerAddress = Address.Create(address!.City, address!.Street).Value;
        var volunteerYearsOfExperience = YearsOfExperience.Create(yearsOfExperience!.Value).Value;
        var volunteerRequisites = requisites
            .Select(r => Requisite.Create(r.Name, r.Description).Value);

        return new Volunteer(
            volunteerId,
            volunteerDescription,
            volunteerAddress,
            volunteerYearsOfExperience,
            volunteerRequisites);
    }
}
using KindPaws.Core.Dtos;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Application.Helpers;

public static class VolunteerHelper
{
    public static Volunteer ForceCreateNewVolunteer(
        FullNameDto fullNameDto,
        string emailAddress,
        string phoneNumber)
    {
        var volunteerId = VolunteerId.CreateRandom();
        var volunteerFullName = FullName.Create(
            fullNameDto.FirstName,
            fullNameDto.LastName,
            fullNameDto.Patronymic).Value;
        var volunteerEmailAddress = EmailAddress.Create(emailAddress).Value;
        var volunteerPhoneNumber = PhoneNumber.Create(phoneNumber).Value;

        return new Volunteer(
            volunteerId,
            volunteerFullName,
            volunteerEmailAddress,
            volunteerPhoneNumber);
    }
}
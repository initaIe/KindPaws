using KindPaws.Application.DTOs;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Helpers;

public static class VolunteerHelper
{
    public static Volunteer ForceCreateNewVolunteer(
        FullNameDTO fullNameDTO,
        string emailAddress,
        string phoneNumber)
    {
        var volunteerId = VolunteerId.CreateRandom();
        var volunteerFullName = FullName.Create(
            fullNameDTO.FirstName,
            fullNameDTO.LastName,
            fullNameDTO.Patronymic).Value;
        var volunteerEmailAddress = EmailAddress.Create(emailAddress).Value;
        var volunteerPhoneNumber = PhoneNumber.Create(phoneNumber).Value;
        
        return new Volunteer(
            volunteerId,
            null,
            null,
            volunteerFullName,
            volunteerEmailAddress,
            volunteerPhoneNumber,
            null,
            null,
            null);
    }
}
using KindPaws.Volunteers.Domain.AggregateRoot;

namespace KindPaws.Volunteers.Application.Helpers;

public static class VolunteerHelper
{
    public static Volunteer ForceCreateNewVolunteer()
        => Volunteer.CreateNew();
}
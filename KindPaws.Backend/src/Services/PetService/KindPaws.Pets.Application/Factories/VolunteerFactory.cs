using KindPaws.Pets.Domain.VolunteersManagement.AggregateRoot;

namespace KindPaws.Pets.Application.Factories;

public static class VolunteerFactory
{
    public static Volunteer ForceCreateNew()
    {
        return Volunteer.CreateNew();
    }
}
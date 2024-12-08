using KindPaws.Users.Domain.UsersManagement.Entities;

namespace KindPaws.Users.Application.Factories;

public static class ProfileFactory
{
    public static Profile ForceCreateNew()
    {
        return Profile.CreateNew();
    }
}
using KindPaws.Roles.Domain.AggregateRoot;

namespace KindPaws.Roles.Application.Helpers;

public static class RoleHelper
{
    public static Role ForceCreateNewRole(string name)
    {
        var roleName = RoleName.Create(name).Value;

        return Role.CreateNew(roleName);
    }
}
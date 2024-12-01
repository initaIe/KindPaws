using KindPaws.Roles.Domain.AggregateRoot;
using KindPaws.Roles.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Roles.Application.Helpers;

public static class RoleHelper
{
    public static Role ForceCreateNewRole(string name)
    {
        var roleName = RoleName.Create(name).Value;

        return Role.CreateNew(roleName);
    }
}
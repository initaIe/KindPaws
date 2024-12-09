using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.Users.Domain.RolesManagement.AggregateRoot;

namespace KindPaws.Users.Application.Factories;

public static class RoleFactory
{
    public static Role ForceCreateNew(
        string name)
    {
        var roleName = RoleName.Create(name).Value;

        return Role.CreateNew(roleName);
    }
}
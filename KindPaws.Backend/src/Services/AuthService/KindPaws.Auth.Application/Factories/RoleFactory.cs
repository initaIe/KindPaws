using KindPaws.Auth.Domain.RolesManagement.AggregateRoot;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Auth.Application.Factories;

public static class RoleFactory
{
    public static Role ForceCreateNew(string name)
    {
        var roleName = RoleName.Create(name).Value;

        return Role.CreateNew(roleName);
    }
}
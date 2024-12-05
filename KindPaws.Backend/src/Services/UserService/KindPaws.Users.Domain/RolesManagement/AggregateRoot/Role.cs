using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Users.Domain.RolesManagement.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Users.Domain.RolesManagement.AggregateRoot;

public sealed class Role : AggregateRoot<RoleId>
{
    // ef core
    private Role(
        RoleId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    private Role(
        RoleId id,
        CreatedAt createdAt,
        RoleName name)
        : base(id, createdAt)
    {
        Name = name;
    }

    public RoleName Name { get; private set; }

    #region Factory methods

    public static Role CreateNew(RoleName name)
    {
        var id = RoleId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new Role(
            id,
            createdAt,
            name);
    }

    #endregion

    #region CRUD

    public void UpdateName(RoleName name)
    {
        Name = name;
    }

    #endregion
}
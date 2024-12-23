using KindPaws.SharedKernel.DDD;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Users.Domain.RolesManagement.AggregateRoot;

public sealed class Role : AggregateRoot<UserRoleId>
{
    #region EF Core constructor

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Role(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        UserRoleId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    #endregion

    private Role(
        UserRoleId id,
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
        var id = UserRoleId.CreateRandom();
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
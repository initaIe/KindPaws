using KindPaws.Auth.Domain.PermissionsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.DDD;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Auth.Domain.PermissionsManagement.AggregateRoot;

public class Permission : AggregateRoot<PermissionId>
{
    #region EF Core constructor

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Permission(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        PermissionId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    #endregion

    private Permission(
        PermissionId id,
        CreatedAt createdAt,
        PermissionCode code)
        : base(id, createdAt)
    {
        Code = code;
    }

    public PermissionCode Code { get; private set; }

    #region Factory methods

    public static Permission CreateNew(PermissionCode code)
    {
        var id = PermissionId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new Permission(
            id,
            createdAt,
            code);
    }

    public static Permission Create(
        PermissionId id,
        CreatedAt createdAt,
        PermissionCode code)
    {
        return new Permission(
            id,
            createdAt,
            code);
    }

    #endregion

    #region CRUD

    public void UpdateCode(PermissionCode code)
    {
        UpdateLastModifiedAt();
        Code = code;
    }

    #endregion
}
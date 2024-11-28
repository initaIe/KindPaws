using KindPaws.Permissions.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Permissions.Domain.AggregateRoot;

public class Permission : IEntity<PermissionId>
{
    // ef core
    private Permission()
    {
    }

    public Permission(
        PermissionId id, 
        PermissionCode code, 
        CreatedAt createdAt)
    {
        Id = id;
        Code = code;
        CreatedAt = createdAt;
    }

    public PermissionId Id { get; private set; }
    public PermissionCode Code { get; private set; }
    public CreatedAt CreatedAt { get; private set; }

    #region Factory methods

    public static Permission CreateNew(PermissionCode code)
    {
        var id = PermissionId.CreateRandom();
        var creationTimestamp = CreatedAt.CreateNew();
        
        return new Permission(id, code, creationTimestamp);
    }

    #endregion
}
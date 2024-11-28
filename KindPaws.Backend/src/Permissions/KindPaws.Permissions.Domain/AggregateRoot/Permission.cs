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
        CreationTimestamp creationTimestamp)
    {
        Id = id;
        Code = code;
        CreationTimestamp = creationTimestamp;
    }

    public PermissionId Id { get; private set; }
    public PermissionCode Code { get; private set; }
    public CreationTimestamp CreationTimestamp { get; private set; }

    #region Factory methods

    public static Permission CreateNew(PermissionCode code)
    {
        var id = PermissionId.CreateRandom();
        var creationTimestamp = CreationTimestamp.CreateNew();
        
        return new Permission(id, code, creationTimestamp);
    }

    #endregion
}
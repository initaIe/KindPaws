using KindPaws.Roles.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Roles.Domain.AggregateRoot;

public sealed class Role : IEntity<RoleId>
{
    // ef core
    public Role()
    {
    }

    private Role(
        RoleId id,
        RoleName name,
        DateTime creationTimestamp)
    {
        Id = id;
        Name = name;
        CreationTimestamp = creationTimestamp;
    }

    public RoleId Id { get; private set; }
    public RoleName Name { get; private set; }
    public DateTime CreationTimestamp { get; private set; }

    public static Role CreateNew(RoleName name)
    {
        var id = RoleId.CreateRandom();
        var creationTimestamp = DateTime.UtcNow;
        return new Role(id, name, creationTimestamp);
    }
    
    public static Result<Role, Error> Create(
        RoleId id,
        RoleName name,
        DateTime creationTimestamp)
    {
        if (creationTimestamp > DateTime.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(creationTimestamp));
        
        return new Role(id, name, creationTimestamp);
    }
}
using KindPaws.Core.Abstractions;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Roles.Application.Abstractions;
using KindPaws.Roles.Domain.AggregateRoot;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Roles.Application.Features.Commands.AddPermission;

public class AddPermissionHandler : ICommandHandler<Guid, AddPermissionCommand>
{
    private readonly IRolesReadDbContext _dbContext;
    private readonly IRepository<Role, RoleId> _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddPermissionCommand command,
        CancellationToken cancellationToken = default)
    {
        var isRoleExist = await _dbContext.Roles.AnyAsync(
            r=>r.Id == command.RoleId,
            cancellationToken);
        
        if (!isRoleExist)
            return Errors.General.RecordNotFound(
                nameof(Role), 
                nameof(Role.Id), 
                command.RoleId)
                .ToErrorList();
        
        var isPermissionExist = await _dbContext.Permissions.AnyAsync(
            r=>r.Id == command.PermissionId,
            cancellationToken);
        
        if (!isPermissionExist)
            return Errors.General.RecordNotFound(
                nameof(Permission), 
                nameof(PermissionId), 
                command.PermissionId)
                .ToErrorList();
        
        var permissionId = PermissionId.Create(command.PermissionId).Value;
        
        var role = await _roleRepository.GetByIdAsync(command.RoleId, cancellationToken);
        var permission = await _permissionRepository.GetByIdAsync(permissionId, cancellationToken);

        role.Value.AddPermission(permission.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return role.Value.Id;
    }
}
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Roles.Application.Abstractions;
using KindPaws.Roles.Domain.AggregateRoot;
using KindPaws.Roles.Domain.Entities;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Roles.Application.Features.RolePermissions.Delete;

public class DeleteRolePermissionHandler : ICommandHandler<Guid, DeleteRolePermissionCommand>
{
    private readonly IRolesReadDbContext _dbContext;
    private readonly IRepository<Role, RoleId> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRolePermissionHandler(
        IRolesReadDbContext dbContext,
        IRepository<Role, RoleId> repository,
        [FromKeyedServices(Modules.Roles)]IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        DeleteRolePermissionCommand command, 
        CancellationToken cancellationToken = default)
    {
        var isRoleExist = await _dbContext.Roles.AnyAsync(
            r => r.Id == command.RoleId,
            cancellationToken);
        
        if (!isRoleExist)
            return Errors.General.RecordNotFound(
                nameof(Role),
                nameof(RoleId), 
                command.RoleId)
                .ToErrorList();
        
        var isRolePermissionExist = await _dbContext.RolePermissions.AnyAsync(
            rp => rp.Id == command.RolePermissionId && rp.RoleId == command.RoleId,
            cancellationToken);
        
        if (!isRolePermissionExist)
            return Errors.General.RecordNotFound(
                    nameof(RolePermission),
                    nameof(RolePermissionId), 
                    command.RolePermissionId)
                .ToErrorList();

        var roleId = RoleId.Create(command.RoleId).Value;
        var rolePermission =await _repository.GetByIdAsync(roleId, cancellationToken);
        
        var rolePermissionId = RolePermissionId.Create(command.RoleId).Value;
        
        var rolePermissionDeletionResult = rolePermission.Value.DeleteRolePermission(rolePermissionId);
        
        if (rolePermissionDeletionResult.IsFailure)
            return rolePermissionDeletionResult.Error.ToErrorList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return rolePermission.Value.Id.Value;
    }
}
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Permissions.Contracts;
using KindPaws.Roles.Application.Abstractions;
using KindPaws.Roles.Application.Helpers;
using KindPaws.Roles.Domain.AggregateRoot;
using KindPaws.Roles.Domain.Entities;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Roles.Application.Features.RolePermissions.Add;

public class AddRolePermissionHandler : ICommandHandler<Guid, AddRolePermissionCommand>
{
    private readonly IRolesReadDbContext _dbContext;
    private readonly IRepository<Role, RoleId> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPermissionsContract _permissionsContract;

    public AddRolePermissionHandler(
        IRolesReadDbContext dbContext,
        IRepository<Role, RoleId> repository,
        [FromKeyedServices(Modules.Roles)] IUnitOfWork unitOfWork,
        IPermissionsContract permissionsContract)
    {
        _dbContext = dbContext;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _permissionsContract = permissionsContract;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddRolePermissionCommand command,
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

        var isPermissionExist = await _permissionsContract.IsPermissionByIdExistAsync(
            command.PermissionId,
            cancellationToken);

        if (!isPermissionExist)
            return Errors.General.RecordNotFound(
                    "Permission",
                    nameof(PermissionId),
                    command.PermissionId)
                .ToErrorList();

        var isRolePermissionAlreadyExist = await _dbContext.RolePermissions.AnyAsync(
            rp => rp.RoleId == command.RoleId && rp.PermissionId == command.PermissionId,
            cancellationToken);

        if (isRolePermissionAlreadyExist)
            return Errors.General.RecordAlreadyExist(nameof(RolePermission), nameof(PermissionId)).ToErrorList();

        var roleId = RoleId.Create(command.RoleId).Value;
        var role = await _repository.GetByIdAsync(roleId, cancellationToken);

        var rolePermission = RolePermissionHelper.ForceCreateNewRolePermission(command.PermissionId);

        role.Value.AddRolePermission(rolePermission);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return rolePermission.Id.Value;
    }
}
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Permissions.Application.Abstractions;
using KindPaws.Permissions.Application.Features.Permissions.Create;
using KindPaws.Permissions.Application.Features.Permissions.Delete;
using KindPaws.Permissions.Contracts;
using KindPaws.Permissions.Contracts.Requests;
using KindPaws.Permissions.Presentation.Mappers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Permissions.Presentation.Contract;

public class PermissionsContract : IPermissionsContract
{
    private readonly IPermissionsReadDbContext _dbContext;
    private readonly ICommandHandler<Guid, CreatePermissionCommand> _createPermissionHandler;
    private readonly ICommandHandler<Guid, DeletePermissionCommand> _deletePermissionHandler;

    public PermissionsContract(
        ICommandHandler<Guid, CreatePermissionCommand> createPermissionHandler,
        IPermissionsReadDbContext dbContext, 
        ICommandHandler<Guid, DeletePermissionCommand> deletePermissionHandler)
    {
        _createPermissionHandler = createPermissionHandler;
        _dbContext = dbContext;
        _deletePermissionHandler = deletePermissionHandler;
    }

    public async Task<Result<Guid, ErrorList>> CreatePermissionAsync(
        CreatePermissionRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();
        return await _createPermissionHandler.HandleAsync(command, cancellationToken);
    }

    public async Task<Result<Guid, ErrorList>> DeletePermissionAsync(
        Guid permissionId,
        CancellationToken cancellationToken = default)
    {
        var command = new DeletePermissionCommand(permissionId);
        return await _deletePermissionHandler.HandleAsync(command, cancellationToken);
    }

    public async Task<bool> IsPermissionByIdExistAsync(
        Guid permissionId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Permissions.AnyAsync(
            p => p.Id == permissionId,
            cancellationToken);
    }
}
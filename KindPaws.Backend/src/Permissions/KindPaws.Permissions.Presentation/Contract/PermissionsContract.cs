using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Permissions.Application.Abstractions;
using KindPaws.Permissions.Application.Features.Permissions.Commands.Create;
using KindPaws.Permissions.Application.Features.Permissions.Commands.Delete;
using KindPaws.Permissions.Application.Features.Permissions.Queries.GetIdByName;
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
    private readonly IQueryHandler<Result<Guid, ErrorList>, GetPermissionIdByCodeQuery> _getPermissionIdByCodeHandler;

    public PermissionsContract(
        ICommandHandler<Guid, CreatePermissionCommand> createPermissionHandler,
        IPermissionsReadDbContext dbContext,
        ICommandHandler<Guid, DeletePermissionCommand> deletePermissionHandler,
        IQueryHandler<Result<Guid, ErrorList>, GetPermissionIdByCodeQuery> getPermissionIdByCodeHandler)
    {
        _createPermissionHandler = createPermissionHandler;
        _dbContext = dbContext;
        _deletePermissionHandler = deletePermissionHandler;
        _getPermissionIdByCodeHandler = getPermissionIdByCodeHandler;
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

    public async Task<Result<Guid, ErrorList>> GetPermissionIdByCodeAsync(
        string permissionCode,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPermissionIdByCodeQuery(permissionCode);
        return await _getPermissionIdByCodeHandler.HandleAsync(query, cancellationToken);
    }
}
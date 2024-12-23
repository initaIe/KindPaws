using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Roles.Application.Abstractions;
using KindPaws.Roles.Application.Features.Roles.Commands.CreateRole;
using KindPaws.Roles.Application.Features.Roles.Commands.DeleteRole;
using KindPaws.Roles.Application.Features.Roles.Queries.GetIdByName;
using KindPaws.Roles.Contracts;
using KindPaws.Roles.Contracts.Requests;
using KindPaws.Roles.Presentation.Mappers;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Roles.Presentation.Contract;

public class RolesContract : IRolesContract
{
    private readonly IRolesReadDbContext _dbContext;
    private readonly ICommandHandler<Guid, CreateRoleCommand> _createRoleHandler;
    private readonly ICommandHandler<Guid, DeleteRoleCommand> _deleteRoleHandler;
    private readonly IQueryHandler<Result<Guid, ErrorList>, GetRoleIdByNameQuery> _getRoleIdByNameHandler;

    public RolesContract(
        IRolesReadDbContext dbContext,
        ICommandHandler<Guid, CreateRoleCommand> createRoleHandler,
        ICommandHandler<Guid, DeleteRoleCommand> deleteRoleHandler,
        IQueryHandler<Result<Guid, ErrorList>, GetRoleIdByNameQuery> getRoleIdByNameHandler)
    {
        _createRoleHandler = createRoleHandler;
        _deleteRoleHandler = deleteRoleHandler;
        _getRoleIdByNameHandler = getRoleIdByNameHandler;
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, ErrorList>> CreateRoleAsync(
        CreateRoleRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();
        return await _createRoleHandler.HandleAsync(command, cancellationToken);
    }

    public async Task<Result<Guid, ErrorList>> DeleteRoleAsync(
        Guid roleId,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteRoleCommand(roleId);
        return await _deleteRoleHandler.HandleAsync(command, cancellationToken);
    }

    public async Task<bool> IsRoleByIdExistAsync(
        Guid roleId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles.AnyAsync(r => r.Id == roleId, cancellationToken);
    }

    public async Task<Result<Guid, ErrorList>> GetRoleIdByNameAsync(
        string roleName,
        CancellationToken cancellationToken = default)
    {
        var query = new GetRoleIdByNameQuery(roleName);
        return await _getRoleIdByNameHandler.HandleAsync(query, cancellationToken);
    }

    public async Task<IReadOnlyList<Guid>> GetPermissionsByRoleIdsAsync(
        IEnumerable<Guid> roleIds,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles
            .Where(r => roleIds.Contains(r.Id))
            .SelectMany(r => r.Permissions)
            .ToListAsync(cancellationToken);
    }
}
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Roles.Application.Abstractions;
using KindPaws.Roles.Application.Features.Roles.Create;
using KindPaws.Roles.Application.Features.Roles.Delete;
using KindPaws.Roles.Contracts;
using KindPaws.Roles.Contracts.Requests;
using KindPaws.Roles.Presentation.Mappers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Roles.Presentation.Contract;

public class RolesContract : IRolesContract
{
    private readonly IRolesReadDbContext _dbContext;
    private readonly ICommandHandler<Guid, CreateRoleCommand> _createRoleHandler;
    private readonly ICommandHandler<Guid, DeleteRoleCommand> _deleteRoleHandler;

    public RolesContract(
        IRolesReadDbContext dbContext,
        ICommandHandler<Guid, CreateRoleCommand> createRoleHandler,
        ICommandHandler<Guid, DeleteRoleCommand> deleteRoleHandler)
    {
        _createRoleHandler = createRoleHandler;
        _deleteRoleHandler = deleteRoleHandler;
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, ErrorList>> CreateRoleAsync(CreateRoleRequest request)
    {
        var command = request.ToCommand();
        return await _createRoleHandler.HandleAsync(command);
    }

    public async Task<Result<Guid, ErrorList>> DeleteRoleAsync(Guid roleId)
    {
        var command = new DeleteRoleCommand(roleId);
        return await _deleteRoleHandler.HandleAsync(command);
    }

    public async Task<bool> IsRoleByIdExist(Guid roleId)
    {
        return await _dbContext.Roles.AnyAsync(r => r.Id == roleId);
    }
}
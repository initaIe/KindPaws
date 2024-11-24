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

namespace KindPaws.Roles.Application.Features.Roles.Commands.DeleteRole;

public class DeleteRoleHandler : ICommandHandler<Guid, DeleteRoleCommand>
{
    private readonly IRolesReadDbContext _dbContext;
    private readonly IRepository<Role, RoleId> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoleHandler(
        IRolesReadDbContext dbContext,
        IRepository<Role, RoleId> repository,
        [FromKeyedServices(Modules.Roles)] IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        DeleteRoleCommand command,
        CancellationToken cancellationToken = default)
    {
        var isRoleExist = await _dbContext.Roles.AnyAsync(
            r => r.Id == command.RoleId,
            cancellationToken);

        if (!isRoleExist)
            return Errors.General.RecordNotFound(nameof(Role), nameof(RoleId), command.RoleId).ToErrorList();

        var roleId = RoleId.Create(command.RoleId).Value;

        var role = await _repository.GetByIdAsync(roleId, cancellationToken);

        _repository.Delete(role.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return role.Value.Id.Value;
    }
}
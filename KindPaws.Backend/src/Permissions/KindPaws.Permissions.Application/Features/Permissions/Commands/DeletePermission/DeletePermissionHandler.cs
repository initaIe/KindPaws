using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Permissions.Application.Abstractions;
using KindPaws.Permissions.Domain.AggregateRoot;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Permissions.Application.Features.Permissions.Commands.DeletePermission;

public class DeletePermissionHandler : ICommandHandler<Guid, DeletePermissionCommand>
{
    private readonly IPermissionsReadDbContext _dbContext;
    private readonly IRepository<Permission, PermissionId> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePermissionHandler(
        IPermissionsReadDbContext dbContext,
        IRepository<Permission, PermissionId> repository,
        [FromKeyedServices(Modules.Permissions)]
        IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        DeletePermissionCommand command,
        CancellationToken cancellationToken = default)
    {
        var isPermissionExist = await _dbContext.Permissions.AnyAsync(
            p => p.Id == command.PermissionId,
            cancellationToken);

        if (!isPermissionExist)
            return GeneralErrors.RecordNotFound(
                    nameof(Permission),
                    nameof(PermissionId),
                    command.PermissionId)
                .ToErrorList();

        var permissionId = PermissionId.Create(command.PermissionId).Value;
        var permission = await _repository.GetByIdAsync(permissionId, cancellationToken);

        _repository.Delete(permission.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return permission.Value.Id.Value;
    }
}
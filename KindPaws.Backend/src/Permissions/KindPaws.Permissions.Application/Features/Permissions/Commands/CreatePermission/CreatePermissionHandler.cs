using KindPaws.Core.Abstractions.Database;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Permissions.Application.Abstractions;
using KindPaws.Permissions.Application.Helpers;
using KindPaws.Permissions.Domain.AggregateRoot;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Permissions.Application.Features.Permissions.Commands.CreatePermission;

public class CreatePermissionHandler : ICommandHandler<Guid, CreatePermissionCommand>
{
    private readonly IPermissionsReadDbContext _dbContext;
    private readonly IRepository<Permission, PermissionId> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePermissionHandler(
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
        CreatePermissionCommand command,
        CancellationToken cancellationToken = default)
    {
        var isPermissionCodeAlreadyTaken = await _dbContext.Permissions.AnyAsync(
            p => p.Code == command.Code,
            cancellationToken);

        if (isPermissionCodeAlreadyTaken)
            return ErrorsGeneral.RecordAlreadyExist(
                    nameof(Permission),
                    nameof(PermissionCode))
                .ToErrorList();

        var permission = PermissionHelper.ForceCreateNewPermission(command.Code);

        await _repository.AddAsync(permission, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return permission.Id.Value;
    }
}
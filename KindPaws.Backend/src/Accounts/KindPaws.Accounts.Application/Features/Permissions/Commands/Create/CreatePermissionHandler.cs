using System.Reflection.Metadata.Ecma335;
using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain.Permission;
using KindPaws.Accounts.Domain.Permission.ValueObjectsManagement.ValueObjects;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Application.Features.Permissions.Commands.Create;

public class CreatePermissionHandler : ICommandHandler<Guid, CreatePermissionCommand>
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly IRepository<Permission, PermissionId> _permissionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePermissionHandler(
        IAccountsReadDbContext dbContext,
        IRepository<Permission, PermissionId> permissionRepository,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _permissionRepository = permissionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        CreatePermissionCommand command,
        CancellationToken cancellationToken = default)
    {
        var code = PermissionCode.Create(command.Code).Value;
        var isPermissionCodeAlreadyTaken = _dbContext.Permissions.Any(p => p.Code == code);
        
        if (isPermissionCodeAlreadyTaken)
            return Errors.General.RecordAlreadyExist(nameof(Permission), nameof(PermissionCode)).ToErrorList();

        var permissionId = PermissionId.CreateRandom();
        var permission = new Permission(permissionId, code);

        await _permissionRepository.AddAsync(permission, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return permission.Id.Value;
    }
}
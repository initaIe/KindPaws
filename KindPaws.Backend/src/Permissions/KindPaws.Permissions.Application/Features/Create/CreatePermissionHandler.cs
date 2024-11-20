using KindPaws.Core.Abstractions;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Permissions.Application.Abstractions;
using KindPaws.Permissions.Domain.AggregateRoot;
using KindPaws.Permissions.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Permissions.Application.Features.Create;

public class CreatePermissionHandler : ICommandHandler<Guid, CreatePermissionCommand>
{
    private readonly IPermissionsReadDbContext _readDbContext;
    private readonly IRepository<Permission, PermissionId> _permissionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePermissionHandler(
        IPermissionsReadDbContext readDbContext,
        IRepository<Permission, PermissionId> permissionRepository,
        [FromKeyedServices(Modules.Permissions)]IUnitOfWork unitOfWork)
    {
        _readDbContext = readDbContext;
        _permissionRepository = permissionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        CreatePermissionCommand command,
        CancellationToken cancellationToken = default)
    {
        var isPermissionCodeAlreadyTaken = _readDbContext.Permissions.Any(p => p.Code == command.Code);
        
        if (isPermissionCodeAlreadyTaken)
            return Errors.General.RecordAlreadyExist(nameof(Permission), nameof(PermissionCode)).ToErrorList();

        var permissionId = PermissionId.CreateRandom();
        var code = PermissionCode.Create(command.Code).Value;
        var permission = new Permission(permissionId, code);

        await _permissionRepository.AddAsync(permission, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return permission.Id.Value;
    }
}
using KindPaws.Core.Abstractions.Database;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Roles.Application.Abstractions;
using KindPaws.Roles.Application.Helpers;
using KindPaws.Roles.Domain.AggregateRoot;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Roles.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleHandler : ICommandHandler<Guid, CreateRoleCommand>
{
    private readonly IRolesReadDbContext _dbContext;
    private readonly IRepository<Role, UserRoleId> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleHandler(
        IRolesReadDbContext dbContext,
        IRepository<Role, UserRoleId> repository,
        [FromKeyedServices(Modules.Roles)] IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        CreateRoleCommand command,
        CancellationToken cancellationToken = default)
    {
        var isRoleNameAlreadyTaken = await _dbContext.Roles.AnyAsync(
            r => r.Name == command.Name,
            cancellationToken);

        if (isRoleNameAlreadyTaken)
            return ErrorsGeneral.RecordAlreadyExist(nameof(Role), nameof(RoleName)).ToErrorList();

        var role = RoleHelper.ForceCreateNewRole(command.Name);

        await _repository.AddAsync(role, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return role.Id.Value;
    }
}
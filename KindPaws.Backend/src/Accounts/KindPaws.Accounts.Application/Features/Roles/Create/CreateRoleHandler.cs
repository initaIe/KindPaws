using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain.Role;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Application.Features.Roles.Create;

public class CreateRoleHandler : ICommandHandler<Guid, CreateRoleCommand>
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly IRepository<Role, Guid> _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleHandler(
        IAccountsReadDbContext dbContext,
        IRepository<Role, Guid> roleRepository,
        [FromKeyedServices(Modules.Accounts)]IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        CreateRoleCommand command,
        CancellationToken cancellationToken = default)
    {
        var isRoleNameAlreadyTaken = await _dbContext.Roles.AnyAsync(r=>r.Name == command.Name, cancellationToken);

        if (isRoleNameAlreadyTaken)
            return Errors.General.RecordAlreadyExist(nameof(Role), nameof(Role.Name)).ToErrorList();
        
        var id = Guid.NewGuid();
        var name = ShortAlphabeticString.Create(command.Name).Value;

        var role = Role.Create(id, name);
        
        await _roleRepository.AddAsync(role.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return role.Value.Id;
    }
}
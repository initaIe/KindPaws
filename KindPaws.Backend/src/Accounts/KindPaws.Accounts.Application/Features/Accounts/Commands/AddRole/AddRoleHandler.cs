using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Accounts.Domain.Role;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Application.Features.Accounts.Commands.AddRole;

public class AddRoleHandler : ICommandHandler<Guid, AddRoleCommand>
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly IRepository<Account, Guid> _accountsRepository;
    private readonly IRepository<Role, Guid> _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddRoleHandler(
        IAccountsReadDbContext dbContext, 
        IRepository<Account, Guid> accountsRepository,
        IRepository<Role, Guid> roleRepository, 
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _accountsRepository = accountsRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddRoleCommand command,
        CancellationToken cancellationToken = default)
    {
        var isAccountExist = await _dbContext.Users.AnyAsync(a => a.Id == command.AccountId, cancellationToken);

        if (!isAccountExist)
            return Errors.General.RecordNotFound(
                nameof(Account), "AccountId", command.AccountId).ToErrorList();

        var isRoleExist = await _dbContext.Roles.AnyAsync(r => r.Id == command.RoleId, cancellationToken);

        if (!isRoleExist)
            return Errors.General.RecordNotFound(
                nameof(Role), "RoleId", command.RoleId).ToErrorList();
        
        var account = await _accountsRepository.GetByIdAsync(command.AccountId, cancellationToken); 
        var role = await _roleRepository.GetByIdAsync(command.RoleId, cancellationToken);

        account.Value.AddRole(role.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return account.Value.Id;
    }
}
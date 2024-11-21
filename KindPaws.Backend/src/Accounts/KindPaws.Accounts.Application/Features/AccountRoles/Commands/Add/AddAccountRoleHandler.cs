using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Application.Helpers;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Accounts.Domain.Entities;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Roles.Contracts;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Application.Features.AccountRoles.Commands.Add;

public class AddAccountRoleHandler : ICommandHandler<Guid, AddAccountRoleCommand>
{
    private readonly IRolesContract _rolesContract;
    private readonly IAccountsReadDbContext _dbContext;
    private readonly IRepository<Account, AccountId> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AddAccountRoleHandler(
        IAccountsReadDbContext dbContext,
        IRepository<Account, AccountId> repository,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork,
        IRolesContract rolesContract)
    {
        _dbContext = dbContext;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _rolesContract = rolesContract;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddAccountRoleCommand command,
        CancellationToken cancellationToken = default)
    {
        var isAccountExist = await _dbContext.Accounts.AnyAsync(
            a => a.Id == command.AccountId,
            cancellationToken: cancellationToken);

        if (!isAccountExist)
            return Errors.General.RecordNotFound(
                    nameof(Account),
                    nameof(AccountId),
                    command.AccountId)
                .ToErrorList();

        var isRoleExist = await _rolesContract.IsRoleByIdExist(command.RoleId);

        if (!isRoleExist)
            return Errors.General.RecordNotFound(
                    "Role",
                    nameof(RoleId),
                    command.RoleId)
                .ToErrorList();

        var isRoleAlreadyAdded = await _dbContext.AccountRoles.AnyAsync(
            ar => ar.AccountId == command.AccountId && ar.RoleId == command.RoleId,
            cancellationToken);

        if (!isRoleAlreadyAdded)
            return Errors.General.RecordAlreadyExist(
                    nameof(AccountRole),
                    nameof(RoleId))
                .ToErrorList();

        var accountId = AccountId.Create(command.AccountId).Value;
        var account = await _repository.GetByIdAsync(accountId, cancellationToken);

        var accountRole = AccountRoleHelper.ForceCreateNewAccountRole(command.RoleId);

        account.Value.AddAccountRole(accountRole);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return accountRole.Id.Value;
    }
}
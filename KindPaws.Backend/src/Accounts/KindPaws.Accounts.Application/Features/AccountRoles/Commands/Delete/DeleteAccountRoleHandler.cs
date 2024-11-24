using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Accounts.Domain.Entities;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Application.Features.AccountRoles.Commands.Delete;

public class DeleteAccountRoleHandler : ICommandHandler<Guid, DeleteAccountRoleCommand>
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly IRepository<Account, AccountId> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAccountRoleHandler(
        IAccountsReadDbContext dbContext,
        IRepository<Account, AccountId> repository,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        DeleteAccountRoleCommand command,
        CancellationToken cancellationToken = default)
    {
        var isAccountExist = await _dbContext.Accounts.AnyAsync(
            a => a.Id == command.AccountId,
            cancellationToken);

        if (!isAccountExist)
            return Errors.General.RecordNotFound(
                    nameof(Account),
                    nameof(AccountId),
                    command.AccountId)
                .ToErrorList();

        var isAccountRoleExist = await _dbContext.AccountRoles.AnyAsync(
            a => a.Id == command.AccountRoleId,
            cancellationToken);

        if (!isAccountRoleExist)
            return Errors.General.RecordNotFound(
                    nameof(AccountRole),
                    nameof(AccountRoleId),
                    command.AccountRoleId)
                .ToErrorList();

        var accountId = AccountId.Create(command.AccountId).Value;
        var account = await _repository.GetByIdAsync(accountId, cancellationToken);

        var accountRoleId = AccountRoleId.Create(command.AccountRoleId).Value;

        account.Value.DeleteAccountRole(accountRoleId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return accountRoleId.Value;
    }
}
using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Core.Abstractions.Database;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Application.Features.Accounts.Commands.DeleteAccount;

public class DeleteAccountHandler : ICommandHandler<Guid, DeleteAccountCommand>
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly IRepository<Account, AccountId> _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAccountHandler(
        IAccountsReadDbContext dbContext,
        IRepository<Account, AccountId> accountRepository,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        DeleteAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var isAccountExist = await _dbContext.Accounts.AnyAsync(
            a => a.Id == command.AccountId,
            cancellationToken);

        if (!isAccountExist)
            return ErrorsGeneral.RecordNotFound(
                    nameof(Account),
                    nameof(AccountId),
                    command.AccountId)
                .ToErrorList();

        var accountId = AccountId.Create(command.AccountId).Value;
        var account = await _accountRepository.GetByIdAsync(accountId, cancellationToken);

        _accountRepository.Delete(account.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return account.Value.Id.Value;
    }
}
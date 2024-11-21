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

namespace KindPaws.Accounts.Application.Features.RefreshSessions.Commands.Delete;

public class DeleteRefreshSessionHandler : ICommandHandler<Guid, DeleteRefreshSessionCommand>
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly IRepository<Account, AccountId> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRefreshSessionHandler(
        IAccountsReadDbContext dbContext,
        IRepository<Account, AccountId> repository,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        DeleteRefreshSessionCommand command,
        CancellationToken cancellationToken = default)
    {
        var isAccountExist = await _dbContext.Accounts.AnyAsync(
            a=>a.Id == command.AccountId,
            cancellationToken);
        
        if (!isAccountExist)
            return Errors.General.RecordNotFound(
                nameof(Account),
                nameof(AccountId),
                command.AccountId).ToErrorList();
        
        var isRefreshSessionExist= await _dbContext.RefreshSessions.AnyAsync(
            rs=>rs.Id == command.RefreshSessionId,
            cancellationToken);
        
        if (!isRefreshSessionExist)
            return Errors.General.RecordNotFound(
                nameof(RefreshSession),
                nameof(RefreshSessionId),
                command.RefreshSessionId).ToErrorList();
        
        var accountId = AccountId.Create(command.AccountId).Value;
        var account = await _repository.GetByIdAsync(accountId, cancellationToken);
        
        var refreshSessionId = RefreshSessionId.Create(command.RefreshSessionId).Value;
        var deletionRefreshSessionResult = account.Value.DeleteRefreshSession(refreshSessionId);

        if (deletionRefreshSessionResult.IsFailure)
            return deletionRefreshSessionResult.Error.ToErrorList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return account.Value.Id.Value;
    }
}
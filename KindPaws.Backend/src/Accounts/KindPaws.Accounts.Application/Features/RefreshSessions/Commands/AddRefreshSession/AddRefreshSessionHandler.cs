using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Application.Helpers;
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

namespace KindPaws.Accounts.Application.Features.RefreshSessions.Commands.AddRefreshSession;

public class AddRefreshSessionHandler : ICommandHandler<Guid, AddRefreshSessionCommand>
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly IRefreshSessionOptionsProvider _refreshSessionOptionsProvider;
    private readonly IRepository<Account, AccountId> _repository;
    private readonly IUnitOfWork _unitOfWork;


    public AddRefreshSessionHandler(
        IAccountsReadDbContext dbContext,
        IRefreshSessionOptionsProvider refreshSessionOptionsProvider,
        IRepository<Account, AccountId> repository,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _refreshSessionOptionsProvider = refreshSessionOptionsProvider;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddRefreshSessionCommand command,
        CancellationToken cancellationToken = default)
    {
        var isAccountByIdExist = await _dbContext.Accounts.AnyAsync(
            a => a.Id == command.AccountId,
            cancellationToken);

        if (!isAccountByIdExist)
            return GeneralErrors.General.RecordNotFound(nameof(Account), nameof(AccountId)).ToErrorList();

        var isRefreshSessionByJtiAlreadyExist = await _dbContext.RefreshSessions.AnyAsync(
            a => a.Jti == command.Jti,
            cancellationToken);

        if (isRefreshSessionByJtiAlreadyExist)
            return GeneralErrors.General.RecordAlreadyExist(nameof(RefreshSession)).ToErrorList();

        var expiresInDays = _refreshSessionOptionsProvider.GetExpireInDays();
        var expiresAt = DateTimeOffset.UtcNow.AddDays(expiresInDays);
        var refreshSession = RefreshSessionHelper.ForceCreateNewRefreshSession(command.Jti, expiresAt);

        var accountId = AccountId.Create(command.AccountId).Value;

        var account = await _repository.GetByIdAsync(accountId, cancellationToken);

        account.Value.AddRefreshSessions([refreshSession]);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return refreshSession.RefreshToken.Value;
    }
}
using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain.Account;
using KindPaws.Accounts.Domain.Account.ValueObjectsManagement.ValueObjects;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Application.Features.Accounts.Commands.AddRefreshSessionToken;

public class AddRefreshSessionHandler : ICommandHandler<Guid, AddRefreshSessionCommand>
{
    private readonly IRefreshTokenSettingsProvider _refreshTokenSettingsProvider;
    private readonly IAccountsReadDbContext _dbContext;
    private readonly IRepository<Account, Guid> _accountsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddRefreshSessionHandler(
        IRefreshTokenSettingsProvider refreshTokenSettingsProvider,
        IAccountsReadDbContext dbContext,
        IRepository<Account, Guid> accountsRepository,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork)
    {
        _refreshTokenSettingsProvider = refreshTokenSettingsProvider;
        _dbContext = dbContext;
        _accountsRepository = accountsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddRefreshSessionCommand command,
        CancellationToken cancellationToken = default)
    {
        var isAccountExist = await _dbContext.Users.AnyAsync(a => a.Id == command.AccountId, cancellationToken);

        if (!isAccountExist)
            return Errors.General.RecordNotFound(
                nameof(Account), 
                "AccountId",
                command.AccountId)
                .ToErrorList();
        
        var account = await _accountsRepository.GetByIdAsync(command.AccountId, cancellationToken);

        var jti = Jti.Create(command.Jti).Value;
        var isRefreshTokenJtiAlreadyTaken = account.Value.HasRefreshSessionByJti(jti);
        
        if (isRefreshTokenJtiAlreadyTaken)
            return Errors.General.RecordAlreadyExist(nameof(RefreshSession), nameof(Jti)).ToErrorList();

        var expiresInDays = _refreshTokenSettingsProvider.Get().ExpiresInDays;
        var refreshSession = RefreshSession.CreateNew(jti, expiresInDays).Value;

        account.Value.AddRefreshSession(refreshSession);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return account.Value.Id;
    }
}
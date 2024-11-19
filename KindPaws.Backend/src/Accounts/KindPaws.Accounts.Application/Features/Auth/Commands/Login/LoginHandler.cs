using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Contracts.Responses;
using KindPaws.Accounts.Domain.Account;
using KindPaws.Accounts.Domain.Account.ValueObjectsManagement.ValueObjects;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Application.Features.Auth.Commands.Login;

public class LoginHandler : ICommandHandler<LoginResponse, LoginCommand>
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly IRepository<Account, Guid> _accountRepository;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<Account> _accountManager;

    public LoginHandler(
        IAccountsReadDbContext dbContext, 
        IRepository<Account, Guid> accountRepository,
        ITokenProvider tokenProvider,
        IUnitOfWork unitOfWork,
        UserManager<Account> accountManager)
    {
        _dbContext = dbContext;
        _accountRepository = accountRepository;
        _tokenProvider = tokenProvider;
        _unitOfWork = unitOfWork;
        _accountManager = accountManager;
    }

    public async Task<Result<LoginResponse, ErrorList>> HandleAsync(
        LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        var isUserExist = await _dbContext.Users.AnyAsync(
            a=>a.Email == command.Email, 
            cancellationToken);

        if (!isUserExist)
            return Errors.Accounts.CredentialsAreInvalid().ToErrorList();
        
        var account = await _dbContext.Users.FirstOrDefaultAsync(
            a=>a.Email == command.Email,
            cancellationToken);
        
        var accountId = account!.Id;
        var user = await _accountRepository.GetByIdAsync(accountId, cancellationToken);
        var isPasswordValid = await _accountManager.CheckPasswordAsync(user.Value, command.Password);
        
        if (!isPasswordValid)
            return Errors.Accounts.CredentialsAreInvalid().ToErrorList();

        var jti = Jti.CreateRandom();
        var accessToken = _tokenProvider.GetAccessToken(
            user.Value.Id.ToString(),
            user.Value.Email!,
            jti.Value.ToString());
        
        
    }
}
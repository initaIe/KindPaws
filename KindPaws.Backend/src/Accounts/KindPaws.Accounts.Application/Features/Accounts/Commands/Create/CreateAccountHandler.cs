using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Application.Features.Accounts.Commands.Create;

public class CreateAccountHandler : ICommandHandler<Guid, CreateAccountCommand>
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly UserManager<Account> _accountManager;

    public CreateAccountHandler(
        IAccountsReadDbContext dbContext,
        UserManager<Account> accountManager)
    {
        _dbContext = dbContext;
        _accountManager = accountManager;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        CreateAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var isUserNameAlreadyTaken = await _dbContext.Users.AnyAsync(
            a=>a.UserName == command.UserName, 
            cancellationToken);

        if (isUserNameAlreadyTaken)
            return Errors.General.RecordAlreadyExist(nameof(Account), nameof(UserName)).ToErrorList();
        
        var isEmailAddressAlreadyTaken = await _dbContext.Users.AnyAsync(
            a=>a.Email == command.EmailAddress, 
            cancellationToken);

        if (isEmailAddressAlreadyTaken)
            return Errors.General.RecordAlreadyExist(nameof(Account), nameof(EmailAddress)).ToErrorList();
        
        // TODO: add VO password

        var id = Guid.NewGuid();
        var userName = UserName.Create(command.UserName).Value;
        var email = EmailAddress.Create(command.EmailAddress).Value;

        var account = Account.Create(id, userName, email);
        await _accountManager.CreateAsync(account.Value, command.Password);
        
        return account.Value.Id;
    }
}
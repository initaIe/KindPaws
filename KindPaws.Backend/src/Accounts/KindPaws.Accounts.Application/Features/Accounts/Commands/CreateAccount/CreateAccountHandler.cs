using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Application.Helpers;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Application.Features.Accounts.Commands.CreateAccount;

public class CreateAccountHandler : ICommandHandler<Guid, CreateAccountCommand>
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly IPasswordHashProvider _passwordHashProvider;
    private readonly IRepository<Account, AccountId> _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountHandler(
        IAccountsReadDbContext dbContext,
        IPasswordHashProvider passwordHashProvider,
        IRepository<Account, AccountId> accountRepository,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _passwordHashProvider = passwordHashProvider;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        CreateAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var isUserNameAlreadyTaken = await _dbContext.Accounts.AnyAsync(
            a => a.UserName == command.UserName,
            cancellationToken);

        if (isUserNameAlreadyTaken)
            return GeneralErrors.General.RecordAlreadyExist(nameof(Account), nameof(UserName)).ToErrorList();

        var isEmailAddressAlreadyTaken = await _dbContext.Accounts.AnyAsync(
            a => a.EmailAddress == command.EmailAddress,
            cancellationToken);

        if (isEmailAddressAlreadyTaken)
            return GeneralErrors.General.RecordAlreadyExist(nameof(Account), nameof(EmailAddress)).ToErrorList();

        var passwordHashString = _passwordHashProvider.GenerateHash(command.Password);
        var account = AccountHelper.ForceCreateNewAccount(command.UserName, command.EmailAddress, passwordHashString);

        await _accountRepository.AddAsync(account, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return account.Id.Value;
    }
}
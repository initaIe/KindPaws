using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Application.Features.Commands.Create;

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
            return Errors.General.RecordAlreadyExist(nameof(Account), nameof(UserName)).ToErrorList();

        var isEmailAddressAlreadyTaken = await _dbContext.Accounts.AnyAsync(
            a => a.EmailAddress == command.EmailAddress,
            cancellationToken);

        if (isEmailAddressAlreadyTaken)
            return Errors.General.RecordAlreadyExist(nameof(Account), nameof(EmailAddress)).ToErrorList();

        var userName = UserName.Create(command.UserName).Value;
        var email = EmailAddress.Create(command.EmailAddress).Value;
        var passwordHashString = _passwordHashProvider.GenerateHash(command.Password);
        var passwordHash = PasswordHash.Create(passwordHashString).Value;

        var account = Account.CreateNew(userName, email, passwordHash);

        await _accountRepository.AddAsync(account, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return account.Id.Value;
    }
}
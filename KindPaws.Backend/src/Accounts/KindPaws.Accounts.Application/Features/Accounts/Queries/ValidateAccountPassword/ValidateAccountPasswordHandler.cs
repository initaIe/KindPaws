using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Application.Features.Accounts.Queries.ValidateAccountPassword;

public class ValidateAccountPasswordHandler
    : IQueryHandler<Result<ErrorList>, ValidateAccountPasswordQuery>
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly IPasswordHashProvider _passwordHashProvider;

    public ValidateAccountPasswordHandler(
        IAccountsReadDbContext dbContext,
        IPasswordHashProvider passwordHashProvider)
    {
        _dbContext = dbContext;
        _passwordHashProvider = passwordHashProvider;
    }

    public async Task<Result<ErrorList>> HandleAsync(
        ValidateAccountPasswordQuery query,
        CancellationToken cancellationToken = default)
    {
        var isAccountExist = await _dbContext.Accounts.AnyAsync(
            a => a.EmailAddress == query.EmailAddress,
            cancellationToken);

        if (!isAccountExist)
            return Errors.General.RecordNotFound(
                    nameof(Account),
                    nameof(AccountId),
                    query.EmailAddress)
                .ToErrorList();

        var account = await _dbContext.Accounts.FirstOrDefaultAsync(
            a => a.EmailAddress == query.EmailAddress,
            cancellationToken);

        var isPasswordValid = _passwordHashProvider.ValidateHash(account!.PasswordHash, query.Password);
        
        if (!isPasswordValid)
            return Errors.General.ValueIsInvalid().ToErrorList(); // TODO: добавить ошибки для аккаунту

        return true;
    }
}
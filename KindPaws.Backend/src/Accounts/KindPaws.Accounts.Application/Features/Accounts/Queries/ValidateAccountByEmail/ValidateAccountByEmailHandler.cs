using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Accounts.Application.Features.Accounts.Queries.ValidateAccountByEmail;

public class ValidateAccountByEmailHandler
    : IQueryHandler<Result<Guid, ErrorList>, ValidateAccountByQuery>
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly IPasswordHashProvider _passwordHashProvider;

    public ValidateAccountByEmailHandler(
        IAccountsReadDbContext dbContext,
        IPasswordHashProvider passwordHashProvider)
    {
        _dbContext = dbContext;
        _passwordHashProvider = passwordHashProvider;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        ValidateAccountByQuery query,
        CancellationToken cancellationToken = default)
    {
        var account = await _dbContext.Accounts.FirstOrDefaultAsync(
            a => a.EmailAddress == query.EmailAddress,
            cancellationToken);

        if (account == null)
            return Errors.General.RecordNotFound(
                    nameof(Account),
                    nameof(AccountId),
                    query.EmailAddress)
                .ToErrorList();

        var isPasswordValid = _passwordHashProvider.ValidateHash(account!.PasswordHash, query.Password);
        
        if (!isPasswordValid)
            return Errors.General.ValueIsInvalid("Password").ToErrorList();

        return account.Id;
    }
}
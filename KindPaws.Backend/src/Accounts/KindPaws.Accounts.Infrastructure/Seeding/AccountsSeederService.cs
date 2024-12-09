using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Application.Helpers;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Accounts.Infrastructure.DbContexts;
using KindPaws.Accounts.Infrastructure.Options;
using KindPaws.Core.Abstractions.Database;
using KindPaws.Roles.Contracts;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KindPaws.Accounts.Infrastructure.Seeding;

public class AccountsSeederService
{
    private readonly AccountsWriteDbContext _accountsWriteDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AccountsSeederOptions _options;
    private readonly IRolesContract _rolesContract;
    private readonly IPasswordHashProvider _passwordHashProvider;
    private readonly ILogger<AccountsSeederService> _logger;

    public AccountsSeederService(
        IOptions<AccountsSeederOptions> options,
        IRolesContract rolesContract,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork,
        IPasswordHashProvider passwordHashProvider,
        AccountsWriteDbContext accountsWriteDbContext,
        ILogger<AccountsSeederService> logger)
    {
        _rolesContract = rolesContract;
        _unitOfWork = unitOfWork;
        _passwordHashProvider = passwordHashProvider;
        _accountsWriteDbContext = accountsWriteDbContext;
        _logger = logger;
        _options = options.Value;
    }

    public async Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting accounts seeding...");

        var accountsSeedData = GetAccountsSeedData();
        await SeedAccountsAsync(accountsSeedData, cancellationToken);

        var accountRolesSeedData = GetAccountRolesSeedData();
        await SeedAccountRolesAsync(accountsSeedData, accountRolesSeedData, cancellationToken);

        _logger.LogInformation("Accounts seeding was ended...");
    }

    private IReadOnlyList<Account> GetAccountsSeedData()
    {
        string[] properties =
        [
            _options.UserName,
            _options.EmailAddress,
            _options.Password
        ];

        if (properties.Any(string.IsNullOrWhiteSpace))
            throw new ApplicationException("Accounts seed data empty.");

        var passwordHash = _passwordHashProvider.GenerateHash(_options.Password);

        var account = AccountHelper.ForceCreateNewAccount(
            _options.UserName,
            _options.EmailAddress,
            passwordHash);

        return [account];
    }

    private async Task SeedAccountsAsync(
        IEnumerable<Account> accounts,
        CancellationToken cancellationToken = default)
    {
        var existingAccountUserNames = await _accountsWriteDbContext.Accounts
            .Select(a => new { a.UserName, a.EmailAddress })
            .ToListAsync(cancellationToken);

        var newAccounts = accounts
            .Where(a => !existingAccountUserNames.Contains(new { a.UserName, a.EmailAddress }))
            .DistinctBy(a => new { a.UserName, a.EmailAddress })
            .ToList();

        if (newAccounts.Count != 0)
        {
            await _accountsWriteDbContext.Accounts.AddRangeAsync(newAccounts, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    private Dictionary<string, string[]> GetAccountRolesSeedData()
    {
        if (string.IsNullOrWhiteSpace(_options.RoleName))
            throw new ApplicationException("AccountRoles seed data empty.");

        Dictionary<string, string[]> accountUserNameRoles = [];
        accountUserNameRoles.Add(_options.UserName, [_options.RoleName]);

        return accountUserNameRoles;
    }

    private async Task SeedAccountRolesAsync(
        IEnumerable<Account> addedAccounts,
        Dictionary<string, string[]> accountUserNameRoles,
        CancellationToken cancellationToken = default)
    {
        addedAccounts = addedAccounts.ToList();

        foreach (var accountRoles in accountUserNameRoles)
        {
            var account = addedAccounts.FirstOrDefault(
                a => a.UserName.Value == accountRoles.Key);

            if (account == null)
                throw new ApplicationException($"Seed account {accountRoles.Key} does not exist.");

            List<Result<Guid, ErrorList>> roleIdResults = [];
            foreach (var roleName in accountRoles.Value)
            {
                var roleId = await _rolesContract
                    .GetRoleIdByNameAsync(roleName, cancellationToken);

                roleIdResults.Add(roleId);
            }

            if (roleIdResults.Any(p => p.IsFailure))
                throw new ApplicationException($"AccountRoles seeding was failure.");

            var accountsRoles = roleIdResults
                .Select(p => UserRoleId.Create(p.Value).Value);

            var newAccountsRoles = accountsRoles
                .Where(ar => !account.Roles.Contains(ar))
                .Distinct()
                .ToList();

            if (newAccountsRoles.Count != 0)
            {
                account.AddRoles(newAccountsRoles);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
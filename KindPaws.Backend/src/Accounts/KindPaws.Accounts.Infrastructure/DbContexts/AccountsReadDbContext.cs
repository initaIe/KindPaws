using EntityFramework.Exceptions.PostgreSQL;
using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Contracts.Dtos;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Accounts.Domain.Entities;
using KindPaws.Core.Factories;
using KindPaws.Core.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Accounts.Infrastructure.DbContexts;

public class AccountsReadDbContext(IOptions<PostgresOptions> postgresOptions)
    : DbContext, IAccountsReadDbContext
{
    private readonly PostgresOptions _postgresOptions = postgresOptions.Value;

    public IQueryable<AccountDto> Accounts => Set<AccountDto>();
    public IQueryable<RefreshSessionDto> RefreshSessions => Set<RefreshSessionDto>();
    public IQueryable<AccountRoleDto> AccountRoles => Set<AccountRoleDto>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_postgresOptions.ConnectionString)
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(LoggerFactories.CreateConsole())
            .UseExceptionProcessor()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("accounts");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AccountsReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
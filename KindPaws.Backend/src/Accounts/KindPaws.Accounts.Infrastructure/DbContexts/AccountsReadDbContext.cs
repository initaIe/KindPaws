using EntityFramework.Exceptions.PostgreSQL;
using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Application.DataModels;
using KindPaws.Core.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Accounts.Infrastructure.DbContexts;

public class AccountsReadDbContext : DbContext, IAccountsReadDbContext
{
    private readonly PostgresOptions _postgresOptions;

    public AccountsReadDbContext(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }

    public IQueryable<AccountDataModel> Accounts => Set<AccountDataModel>();
    public IQueryable<RefreshSessionDataModel> RefreshSessions => Set<RefreshSessionDataModel>();

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
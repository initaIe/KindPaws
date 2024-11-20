using EntityFramework.Exceptions.PostgreSQL;
using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Core.Factories;
using KindPaws.Core.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Accounts.Infrastructure.DbContexts;

public class AccountsReadDbContext(IOptions<PostgresOptions> postgresOptions) 
    : DbContext, IAccountsReadDbContext
{
    public IQueryable<Account> Accounts => Set<Account>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(postgresOptions.Value.ConnectionString)
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(LoggerFactories.CreateConsole())
            .UseExceptionProcessor()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("accounts");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AccountsWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
using EntityFramework.Exceptions.PostgreSQL;
using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Domain.Entities;
using KindPaws.Framework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KindPaws.Accounts.Infrastructure.DbContexts;

public class AccountsReadDbContext(IOptions<PostgresOptions> postgresOptions)
    : IdentityDbContext<User, Role, Guid>, IAccountsReadDbContext
{
    public IQueryable<RolePermission> RolePermissions => Set<RolePermission>();
    public IQueryable<Permission> Permissions => Set<Permission>();
    public IQueryable<RefreshSession> RefreshSessions => Set<RefreshSession>();
    public new IQueryable<User> Users => Set<User>();
    public new IQueryable<Role> Roles => Set<Role>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(postgresOptions.Value.ConnectionString)
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(CreateLoggerFactory())
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

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}
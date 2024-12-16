using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Application.DataModels;
using KindPaws.Auth.Infrastructure.Options;
using KindPaws.Core.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Auth.Infrastructure.DbContexts;

public class AuthReadDbContext : DbContext, IAuthReadDbContext
{
    private readonly PostgresOptions _postgresOptions;

    public AuthReadDbContext(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }

    public IQueryable<AccountDataModel> Accounts => Set<AccountDataModel>();
    public IQueryable<RoleDataModel> Roles => Set<RoleDataModel>();
    public IQueryable<PermissionDataModel> Permissions => Set<PermissionDataModel>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_postgresOptions.ConnectionString)
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(LoggerFactories.CreateConsole())
            .EnableSensitiveDataLogging()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("auth");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AuthReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
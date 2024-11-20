using EntityFramework.Exceptions.PostgreSQL;
using KindPaws.Accounts.Contracts.Dtos;
using KindPaws.Core.Factories;
using KindPaws.Core.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Permissions.Infrastructure.DbContexts;

public class PermissionsReadDbContext(IOptions<PostgresOptions> postgresOptions) : DbContext
{
    private readonly PostgresOptions _postgresOptions = postgresOptions.Value;
    
    public IQueryable<PermissionDto> Permissions => Set<PermissionDto>();

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
        modelBuilder.HasDefaultSchema("permissions");
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PermissionsWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
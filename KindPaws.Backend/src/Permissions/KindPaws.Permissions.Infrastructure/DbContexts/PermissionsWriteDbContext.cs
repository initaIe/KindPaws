using EntityFramework.Exceptions.PostgreSQL;
using KindPaws.Core.Factories;
using KindPaws.Permissions.Domain.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Permissions.Infrastructure.DbContexts;

public class PermissionsWriteDbContext : DbContext
{
    private readonly PostgresOptions _postgresOptions;

    public PermissionsWriteDbContext(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }

    public DbSet<Permission> Permissions => Set<Permission>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_postgresOptions.ConnectionString)
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(LoggerFactories.CreateConsole())
            .UseExceptionProcessor();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("permissions");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PermissionsWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }
}
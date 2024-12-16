using EntityFramework.Exceptions.PostgreSQL;
using KindPaws.Core.Factories;
using KindPaws.Roles.Domain.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Roles.Infrastructure.DbContexts;

public class RolesWriteDbContext : DbContext
{
    private readonly PostgresOptions _postgresOptions;

    public RolesWriteDbContext(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }

    public DbSet<Role> Roles => Set<Role>();

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
        modelBuilder.HasDefaultSchema("roles");
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(RolesWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }
}
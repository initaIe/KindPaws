using KindPaws.Core.Factories;
using KindPaws.Core.Options;
using KindPaws.Roles.Application.Abstractions;
using KindPaws.Roles.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Roles.Infrastructure.DbContexts;

public class RolesReadDbContext : DbContext, IRolesReadDbContext
{
    private readonly PostgresOptions _postgresOptions;

    public RolesReadDbContext(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }

    public IQueryable<RoleDataModel> Roles => Set<RoleDataModel>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_postgresOptions.ConnectionString)
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(LoggerFactories.CreateConsole())
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("roles");
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(RolesReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
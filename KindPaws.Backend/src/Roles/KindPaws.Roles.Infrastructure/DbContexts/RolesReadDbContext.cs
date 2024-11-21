using EntityFramework.Exceptions.PostgreSQL;
using KindPaws.Core.Factories;
using KindPaws.Core.Options;
using KindPaws.Roles.Application.Abstractions;
using KindPaws.Roles.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Roles.Infrastructure.DbContexts;

public class RolesReadDbContext(IOptions<PostgresOptions> postgresOptions)
    : DbContext, IRolesReadDbContext
{
    private readonly PostgresOptions _postgresOptions = postgresOptions.Value;

    public IQueryable<RoleDto> Roles => Set<RoleDto>();
    public IQueryable<RolePermissionDto> RolePermissions => Set<RolePermissionDto>();

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
        modelBuilder.HasDefaultSchema("roles");
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(RolesReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
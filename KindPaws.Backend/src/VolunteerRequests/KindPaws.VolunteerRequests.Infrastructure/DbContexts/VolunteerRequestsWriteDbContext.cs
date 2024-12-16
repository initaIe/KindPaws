using EntityFramework.Exceptions.PostgreSQL;
using KindPaws.Core.Factories;
using KindPaws.VolunteerRequests.Domain.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.VolunteerRequests.Infrastructure.DbContexts;

public class VolunteerRequestsWriteDbContext : DbContext
{
    private readonly PostgresOptions _postgresOptions;

    public VolunteerRequestsWriteDbContext(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }

    public DbSet<VolunteerRequest> VolunteerRequests => Set<VolunteerRequest>();

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
        modelBuilder.HasDefaultSchema("volunteer_requests");
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(VolunteerRequestsWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }
}
using EntityFramework.Exceptions.PostgreSQL;
using KindPaws.Core.Factories;
using KindPaws.VolunteerRequests.Application.Abstractions;
using KindPaws.VolunteerRequests.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.VolunteerRequests.Infrastructure.DbContexts;

public class VolunteerRequestsReadDbContext
    : DbContext, IVolunteerRequestsReadDbContext
{
    private readonly PostgresOptions _postgresOptions;

    public VolunteerRequestsReadDbContext(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }

    public IQueryable<VolunteerRequestDataModel> VolunteerRequests => Set<VolunteerRequestDataModel>();

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
        modelBuilder.HasDefaultSchema("volunteer_requests");
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(VolunteerRequestsReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
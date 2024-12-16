using EntityFramework.Exceptions.PostgreSQL;
using KindPaws.Core.Factories;
using KindPaws.Volunteers.Application.Abstractions;
using KindPaws.Volunteers.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Volunteers.Infrastructure.DbContexts;

public class VolunteersReadDbContext : DbContext, IVolunteersReadDbContext
{
    private readonly PostgresOptions _postgresOptions;

    public VolunteersReadDbContext(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }

    public IQueryable<VolunteerDataModel> Volunteers => Set<VolunteerDataModel>();
    public IQueryable<PetDataModel> Pets => Set<PetDataModel>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_postgresOptions.ConnectionString)
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(LoggerFactories.CreateConsole())
            .EnableSensitiveDataLogging()
            .UseExceptionProcessor()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("volunteers");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(VolunteersReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
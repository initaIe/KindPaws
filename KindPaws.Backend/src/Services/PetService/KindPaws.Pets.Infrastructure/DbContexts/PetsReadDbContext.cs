using KindPaws.Core.Factories;
using KindPaws.Core.Options;
using KindPaws.Pets.Application.Abstractions;
using KindPaws.Pets.Application.DataModels;
using KindPaws.Pets.Domain.SpeciesManagement.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Pets.Infrastructure.DbContexts;

public class PetsReadDbContext : DbContext, IPetsReadDbContext
{
    private readonly PostgresOptions _postgresOptions;

    public PetsReadDbContext(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }

    public IQueryable<VolunteerDataModel> Volunteers => Set<VolunteerDataModel>();
    public IQueryable<PetDataModel> Pets => Set<PetDataModel>();
    
    public IQueryable<SpecieDataModel> Species => Set<SpecieDataModel>();
    public IQueryable<BreedDataModel> Breeds => Set<BreedDataModel>();

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
        modelBuilder.HasDefaultSchema("users");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PetsReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
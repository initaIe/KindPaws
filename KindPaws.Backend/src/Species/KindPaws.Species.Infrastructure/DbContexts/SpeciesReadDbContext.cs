using EntityFramework.Exceptions.PostgreSQL;
using KindPaws.Core;
using KindPaws.Core.Dtos;
using KindPaws.Species.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KindPaws.Species.Infrastructure.DbContexts;

public class SpeciesReadDbContext(IConfiguration configuration)
    : DbContext, ISpeciesReadDbContext
{
    public IQueryable<SpecieDto> Species => Set<SpecieDto>();
    public IQueryable<BreedDto> Breeds => Set<BreedDto>();

    private ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder => { builder.AddConsole(); });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(configuration.GetConnectionString(Constants.Database.Postgres))
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(CreateLoggerFactory())
            .EnableSensitiveDataLogging()
            .UseExceptionProcessor()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("species");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(SpeciesReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
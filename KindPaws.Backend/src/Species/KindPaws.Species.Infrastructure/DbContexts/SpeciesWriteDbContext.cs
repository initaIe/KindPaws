using KindPaws.Core;
using KindPaws.Species.Domain.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KindPaws.Species.Infrastructure.DbContexts;

public class SpeciesWriteDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<Specie> Species => Set<Specie>();

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
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.HasDefaultSchema("species");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(SpeciesWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }
}
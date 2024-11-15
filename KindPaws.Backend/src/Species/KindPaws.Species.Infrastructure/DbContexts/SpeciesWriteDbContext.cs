using EntityFramework.Exceptions.PostgreSQL;
using KindPaws.Framework.Options;
using KindPaws.Species.Domain.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KindPaws.Species.Infrastructure.DbContexts;

public class SpeciesWriteDbContext(IOptions<PostgresOptions> postgresOptions)
    : DbContext
{
    public DbSet<Specie> Species => Set<Specie>();

    private ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder => { builder.AddConsole(); });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(postgresOptions.Value.ConnectionString)
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(CreateLoggerFactory())
            .EnableSensitiveDataLogging()
            .UseExceptionProcessor();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("species");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(SpeciesWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }
}
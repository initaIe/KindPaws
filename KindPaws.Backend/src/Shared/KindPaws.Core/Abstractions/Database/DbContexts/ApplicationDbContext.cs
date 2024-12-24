using KindPaws.Core.Factories;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Core.Abstractions.Database.DbContexts;

public abstract class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(LoggerFactories.CreateConsole())
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SchemaName);

        modelBuilder.ApplyConfigurationsFromAssembly(
            GetType().Assembly,
            type => type.FullName?.Contains(ConfigurationNamespace) ?? false);
    }

    protected abstract string SchemaName { get; }
    protected abstract string ConfigurationNamespace { get; }
}
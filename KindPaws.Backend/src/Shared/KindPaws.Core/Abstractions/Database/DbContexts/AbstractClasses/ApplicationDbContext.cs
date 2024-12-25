using KindPaws.Core.Abstractions.Database.DbContexts.Interfaces;
using KindPaws.Core.Factories;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Core.Abstractions.Database.DbContexts.AbstractClasses;

public abstract class ApplicationDbContext : DbContext, IApplicationDbContext
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
        modelBuilder.HasDefaultSchema(GetSchemaName());

        modelBuilder.ApplyConfigurationsFromAssembly(
            GetType().Assembly,
            type => type.FullName?.Contains(GetConfigurationNamespace()) ?? false);
    }

    public abstract string GetSchemaName();
    public abstract string GetConfigurationNamespace();
}
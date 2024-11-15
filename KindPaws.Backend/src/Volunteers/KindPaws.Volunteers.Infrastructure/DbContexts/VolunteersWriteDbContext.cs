using EntityFramework.Exceptions.PostgreSQL;
using KindPaws.Framework.Options;
using KindPaws.Volunteers.Domain.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KindPaws.Volunteers.Infrastructure.DbContexts;

public class VolunteersWriteDbContext(IOptions<PostgresOptions> postgresOptions)
    : DbContext
{
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();

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
        modelBuilder.HasDefaultSchema("volunteers");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(VolunteersWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }
}
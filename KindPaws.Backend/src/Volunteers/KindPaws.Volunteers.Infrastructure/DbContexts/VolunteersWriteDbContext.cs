using KindPaws.Core;
using KindPaws.Volunteers.Domain.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Infrastructure.DbContexts;

public class VolunteersWriteDbContext(
    IConfiguration configuration,
    IServiceProvider serviceProvider)
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
            .UseNpgsql(configuration.GetConnectionString(Constants.Database.Postgres))
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(CreateLoggerFactory())
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.HasDefaultSchema("volunteers");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(VolunteersWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }
}
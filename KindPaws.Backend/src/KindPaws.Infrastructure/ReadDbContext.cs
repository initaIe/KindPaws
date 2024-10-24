using KindPaws.Application.Abstractions;
using KindPaws.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KindPaws.Infrastructure;

public class ReadDbContext(IConfiguration configuration)
    : DbContext, IReadDbContext
{
    public DbSet<VolunteerDTO> Volunteers => Set<VolunteerDTO>();
    public DbSet<PetDTO> Pets => Set<PetDTO>();
    public DbSet<SpecieDTO> Species => Set<SpecieDTO>();
    public DbSet<BreedDTO> Breeds => Set<BreedDTO>();

    private ILoggerFactory CreateFactory()
    {
        return LoggerFactory.Create(builder => { builder.AddConsole(); });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(configuration.GetConnectionString(Constants.Database.Postgres))
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(CreateFactory())
            .EnableSensitiveDataLogging()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
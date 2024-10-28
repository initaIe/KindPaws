using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KindPaws.Infrastructure;

public class ReadDbContext(IConfiguration configuration)
    : DbContext, IReadDbContext
{
    public IQueryable<VolunteerDTO> Volunteers => Set<VolunteerDTO>();
    public IQueryable<PetDTO> Pets => Set<PetDTO>();
    public IQueryable<SpecieDTO> Species => Set<SpecieDTO>();
    public IQueryable<BreedDTO> Breeds => Set<BreedDTO>();

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
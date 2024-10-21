using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Infrastructure;

public class ApplicationDbContext(
    IConfiguration configuration,
    IServiceProvider serviceProvider)
    : DbContext
{
    private const string Postgres = nameof(Postgres);

    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<Specie> Species => Set<Specie>();

    private ILoggerFactory CreateFactory()
    {
        return LoggerFactory.Create(builder => { builder.AddConsole(); });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(configuration.GetConnectionString(Postgres))
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(CreateFactory())
            .EnableSensitiveDataLogging()
            .AddInterceptors(serviceProvider.GetRequiredService<SoftDeleteInterceptor>());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
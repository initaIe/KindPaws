using KindPaws.Domain.Managements.SpecieManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteerManagement.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KindPaws.Infrastructure;

public class ApplicationDbContext(IConfiguration configuration) : DbContext
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
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(Postgres));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
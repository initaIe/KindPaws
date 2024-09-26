using KindPaws.Domain.Managements.PetManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteerManagement.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KindPaws.Infrastructure;

public class ApplicationDbContext(IConfiguration configuration) : DbContext
{
    private const string Postgres = nameof(Postgres);

    private ILoggerFactory CreateFactory()
        => LoggerFactory.Create(builder => { builder.AddConsole(); });

    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<Pet> Pets => Set<Pet>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(Postgres));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateFactory());
    }
}
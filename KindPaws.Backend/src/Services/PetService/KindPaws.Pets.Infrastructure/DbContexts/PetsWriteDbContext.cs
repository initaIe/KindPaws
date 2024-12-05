using KindPaws.Core.Factories;
using KindPaws.Core.Options;
using KindPaws.Pets.Domain.SpeciesManagement.AggregateRoot;
using KindPaws.Pets.Domain.VolunteersManagement.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Pets.Infrastructure.DbContexts
{
    public class PetsWriteDbContext : DbContext
    {
        private readonly PostgresOptions _postgresOptions;

        public PetsWriteDbContext(IOptions<PostgresOptions> postgresOptions)
        {
            _postgresOptions = postgresOptions.Value;
        }

        public DbSet<Volunteer> Volunteers => Set<Volunteer>();
        public DbSet<Specie> Species => Set<Specie>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_postgresOptions.ConnectionString)
                .UseSnakeCaseNamingConvention()
                .UseLoggerFactory(LoggerFactories.CreateConsole())
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("pets");

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(PetsWriteDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Write") ?? false);
        }
    }
}
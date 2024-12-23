using KindPaws.Core.Factories;
using KindPaws.Core.OutBox.Abstractions;
using KindPaws.Core.OutBox.Database;
using KindPaws.Core.OutBox.Entities;
using KindPaws.Pets.Domain.SpeciesManagement.AggregateRoot;
using KindPaws.Pets.Domain.VolunteersManagement.AggregateRoot;
using KindPaws.Pets.Infrastructure.Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace KindPaws.Pets.Infrastructure.Persistence.DbContexts
{
    public class PetsWriteDbContext : DbContext, IOutBoxWriteDbContext
    {
        private readonly PostgresOptions _postgresOptions;
        private readonly ISaveChangesInterceptor _saveChangesInterceptor;

        public PetsWriteDbContext(
            IOptions<PostgresOptions> postgresOptions,
            ISaveChangesInterceptor saveChangesInterceptor)
        {
            _saveChangesInterceptor = saveChangesInterceptor;
            _postgresOptions = postgresOptions.Value;
        }

        public DbSet<Volunteer> Volunteers => Set<Volunteer>();
        public DbSet<Specie> Species => Set<Specie>();
        public DbSet<OutBoxMessage> OutBoxMessages => Set<OutBoxMessage>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_postgresOptions.ConnectionString)
                .UseSnakeCaseNamingConvention()
                .UseLoggerFactory(LoggerFactories.CreateConsole())
                .EnableSensitiveDataLogging()
                .AddInterceptors(_saveChangesInterceptor);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("pets");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OutBoxMessagesConfiguration).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(PetsWriteDbContext).Assembly,
                type => type.FullName?.Contains("Persistence.Configurations.Write") ?? false);
        }
    }
}
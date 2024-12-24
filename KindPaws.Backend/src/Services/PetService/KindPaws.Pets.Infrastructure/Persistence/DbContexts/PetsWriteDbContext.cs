using KindPaws.Core.Abstractions.Database.DbContexts;
using KindPaws.Pets.Domain.SpeciesManagement.AggregateRoot;
using KindPaws.Pets.Domain.VolunteersManagement.AggregateRoot;
using KindPaws.Pets.Infrastructure.Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace KindPaws.Pets.Infrastructure.Persistence.DbContexts
{
    public class PetsWriteDbContext : OutBoxWriteDbContext
    {
        private readonly PostgresOptions _postgresOptions;

        public PetsWriteDbContext(
            ISaveChangesInterceptor saveChangesInterceptor,
            IOptions<PostgresOptions> postgresOptions)
            : base(saveChangesInterceptor)
        {
            _postgresOptions = postgresOptions.Value;
        }

        protected override string SchemaName => "pets";
        protected override string ConfigurationNamespace => "Persistence.Configurations.Write";

        public DbSet<Volunteer> Volunteers => Set<Volunteer>();
        public DbSet<Specie> Species => Set<Specie>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_postgresOptions.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
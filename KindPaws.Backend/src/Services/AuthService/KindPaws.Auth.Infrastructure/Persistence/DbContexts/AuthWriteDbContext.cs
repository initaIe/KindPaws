using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Auth.Domain.PermissionsManagement.AggregateRoot;
using KindPaws.Auth.Domain.RolesManagement.AggregateRoot;
using KindPaws.Auth.Infrastructure.Common.Options;
using KindPaws.Auth.Infrastructure.Persistence.Seeding;
using KindPaws.Core.MessageBox.Abstractions.AbstractClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace KindPaws.Auth.Infrastructure.Persistence.DbContexts
{
    public class AuthWriteDbContext : GeneralBoxDbContext
    {
        private readonly PostgresOptions _postgresOptions;

        public AuthWriteDbContext(
            ISaveChangesInterceptor saveChangesInterceptor,
            IOptions<PostgresOptions> postgresOptions)
            : base(saveChangesInterceptor)
        {
            _postgresOptions = postgresOptions.Value;
        }

        public override string GetSchemaName() => "auth";
        public override string GetConfigurationNamespace() => "Persistence.Configurations.Write";

        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_postgresOptions.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedRoles();
        }
    }
}
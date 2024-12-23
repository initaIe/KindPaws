using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Auth.Domain.PermissionsManagement.AggregateRoot;
using KindPaws.Auth.Domain.RolesManagement.AggregateRoot;
using KindPaws.Auth.Infrastructure.Common.Options;
using KindPaws.Auth.Infrastructure.Persistence.Seeding;
using KindPaws.Core.Factories;
using KindPaws.Core.OutBox.Abstractions;
using KindPaws.Core.OutBox.Database;
using KindPaws.Core.OutBox.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace KindPaws.Auth.Infrastructure.Persistence.DbContexts
{
    public class AuthWriteDbContext : DbContext, IOutBoxWriteDbContext
    {
        private readonly PostgresOptions _postgresOptions;
        private readonly ISaveChangesInterceptor _saveChangesInterceptor;

        public AuthWriteDbContext(
            IOptions<PostgresOptions> postgresOptions,
            ISaveChangesInterceptor saveChangesInterceptor)
        {
            _saveChangesInterceptor = saveChangesInterceptor;
            _postgresOptions = postgresOptions.Value;
        }

        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Role> Roles => Set<Role>();

        public DbSet<Permission> Permissions => Set<Permission>();
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
            modelBuilder.HasDefaultSchema("auth");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OutBoxMessagesConfiguration).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AuthWriteDbContext).Assembly,
                type => type.FullName?.Contains("Persistence.Configurations.Write") ?? false);

            modelBuilder.SeedRoles();
        }
    }
}
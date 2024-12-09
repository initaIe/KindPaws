using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Auth.Domain.PermissionsManagement.AggregateRoot;
using KindPaws.Auth.Domain.RolesManagement.AggregateRoot;
using KindPaws.Auth.Infrastructure.Options;
using KindPaws.Auth.Infrastructure.OutBox;
using KindPaws.Core.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Auth.Infrastructure.DbContexts
{
    public class AuthWriteDbContext : DbContext
    {
        private readonly PostgresOptions _postgresOptions;

        public AuthWriteDbContext(IOptions<PostgresOptions> postgresOptions)
        {
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
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("auth");

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AuthWriteDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Write") ?? false);
        }
    }
}
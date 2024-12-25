using KindPaws.Core.MessageBox.Abstractions.AbstractClasses;
using KindPaws.Users.Domain.RolesManagement.AggregateRoot;
using KindPaws.Users.Domain.UsersManagement.AggregateRoot;
using KindPaws.Users.Domain.VolunteerRequestManagement.AggregateRoot;
using KindPaws.Users.Infrastructure.Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace KindPaws.Users.Infrastructure.Persistence.DbContexts
{
    public class UsersWriteDbContext : GeneralBoxDbContext
    {
        private readonly PostgresOptions _postgresOptions;

        public UsersWriteDbContext(
            ISaveChangesInterceptor saveChangesInterceptor,
            IOptions<PostgresOptions> postgresOptions)
            : base(saveChangesInterceptor)
        {
            _postgresOptions = postgresOptions.Value;
        }

        public override string GetSchemaName() => "users";
        public override string GetConfigurationNamespace() => "Persistence.Configurations.Write";

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<VolunteerRequest> VolunteerRequests => Set<VolunteerRequest>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_postgresOptions.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
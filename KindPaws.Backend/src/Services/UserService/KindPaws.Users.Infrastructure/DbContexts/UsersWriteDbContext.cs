using KindPaws.Core.Factories;
using KindPaws.Core.Options;
using KindPaws.Users.Domain.RolesManagement.AggregateRoot;
using KindPaws.Users.Domain.UsersManagement.AggregateRoot;
using KindPaws.Users.Domain.VolunteerRequestManagement.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Users.Infrastructure.DbContexts;

public class UsersWriteDbContext : DbContext
{
    private readonly PostgresOptions _postgresOptions;

    public UsersWriteDbContext(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<VolunteerRequest> VolunteerRequests => Set<VolunteerRequest>();

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
        modelBuilder.HasDefaultSchema("users");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(UsersWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }
}
using KindPaws.Core.Factories;
using KindPaws.Users.Application.Abstractions;
using KindPaws.Users.Application.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Users.Infrastructure.DbContexts;

public class UsersReadDbContext : DbContext, IUsersReadDbContext
{
    private readonly PostgresOptions _postgresOptions;

    public UsersReadDbContext(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }

    public IQueryable<UserDataModel> Users => Set<UserDataModel>();
    public IQueryable<ProfileDataModel> Profiles => Set<ProfileDataModel>();
    public IQueryable<RoleDataModel> Roles => Set<RoleDataModel>();
    public IQueryable<VolunteerRequestDataModel> VolunteerRequests => Set<VolunteerRequestDataModel>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_postgresOptions.ConnectionString)
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(LoggerFactories.CreateConsole())
            .EnableSensitiveDataLogging()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(UsersReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
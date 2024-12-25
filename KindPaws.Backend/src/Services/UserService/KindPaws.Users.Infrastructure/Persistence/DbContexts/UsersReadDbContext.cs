using KindPaws.Core.Abstractions.Database.DbContexts.AbstractClasses;
using KindPaws.Users.Application.Abstractions;
using KindPaws.Users.Application.Common.DataModels;
using KindPaws.Users.Infrastructure.Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Users.Infrastructure.Persistence.DbContexts;

public class UsersReadDbContext : ReadDbContext, IUsersReadDbContext
{
    private readonly PostgresOptions _postgresOptions;

    public UsersReadDbContext(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }
    public override string GetSchemaName() => "users";
    public override string GetConfigurationNamespace() => "Persistence.Configurations.Read";

    public IQueryable<UserDataModel> Users => Set<UserDataModel>();
    public IQueryable<ProfileDataModel> Profiles => Set<ProfileDataModel>();
    public IQueryable<RoleDataModel> Roles => Set<RoleDataModel>();
    public IQueryable<VolunteerRequestDataModel> VolunteerRequests => Set<VolunteerRequestDataModel>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_postgresOptions.ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }
}
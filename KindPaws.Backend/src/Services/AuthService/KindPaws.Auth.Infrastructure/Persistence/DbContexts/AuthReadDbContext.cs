using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Application.Common.DataModels;
using KindPaws.Auth.Infrastructure.Common.Options;
using KindPaws.Core.Abstractions.Database.DbContexts.AbstractClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Auth.Infrastructure.Persistence.DbContexts;

public class AuthReadDbContext : ReadDbContext, IAuthReadDbContext
{
    private readonly PostgresOptions _postgresOptions;

    public AuthReadDbContext(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }

    public IQueryable<AccountDataModel> Accounts => Set<AccountDataModel>();
    public IQueryable<RefreshSessionDataModel> RefreshSessions => Set<RefreshSessionDataModel>();
    public IQueryable<RoleDataModel> Roles => Set<RoleDataModel>();
    public IQueryable<PermissionDataModel> Permissions => Set<PermissionDataModel>();

    public override string GetSchemaName() => "auth";
    public override string GetConfigurationNamespace() => "Persistence.Configurations.Read";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_postgresOptions.ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }
}
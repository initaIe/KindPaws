using System.Text.Json;
using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Infrastructure.Managers;
using KindPaws.Accounts.Infrastructure.Options;
using KindPaws.Accounts.Infrastructure.Seeding.Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KindPaws.Accounts.Infrastructure.Seeding;

public class AccountsSeeder
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<AccountsSeeder> _logger;
    private readonly AccountsSeederOptions _accountsSeederOptions;

    public AccountsSeeder(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<AccountsSeeder> logger,
        IOptions<AccountsSeederOptions> options)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _accountsSeederOptions = options.Value;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting seed accounts...");

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var permissionManager = scope.ServiceProvider.GetRequiredService<PermissionManager>();
        var permissionsSeedData = await GetPermissionsSeedDataAsync(cancellationToken);
        await SeedPermissionsAsync(
            permissionsSeedData,
            permissionManager,
            cancellationToken);

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        var rolesSeedData = await GetRolesSeedDataAsync(cancellationToken);
        await SeedRolesAsync(
            rolesSeedData,
            roleManager);

        var rolePermissionManager = scope.ServiceProvider.GetRequiredService<RolePermissionManager>();
        var rolePermissionSeedData = await GetRolePermissionsSeedDataAsync(cancellationToken);
        await SeedRolePermissionsAsync(
            permissionsSeedData,
            rolesSeedData,
            rolePermissionSeedData,
            rolePermissionManager,
            cancellationToken);

        _logger.LogInformation("Accounts seeding ended.");
    }

    private async Task<List<PermissionConfig>> GetPermissionsSeedDataAsync(
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting reading permissions...");

        var permissionsJson = await File.ReadAllTextAsync(_accountsSeederOptions.PermissionsPath, cancellationToken);

        var permissionsSeedData = JsonSerializer.Deserialize<List<PermissionConfig>>
                                      (permissionsJson, JsonSerializerOptions.Default)
                                  ?? throw new ApplicationException("Permissions json is empty.");

        _logger.LogInformation("Permissions reading ended.");

        return permissionsSeedData;
    }

    private async Task SeedPermissionsAsync(
        IEnumerable<PermissionConfig> permissionsSeedData,
        PermissionManager permissionManager,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting seed permissions.");

        var permissions = permissionsSeedData.Select(permissionSeedData => new Permission
        {
            Id = Guid.NewGuid(),
            Code = permissionSeedData.Code
        });

        await permissionManager.AddRangeIfByCodeNotExistsAsync(permissions, cancellationToken);

        _logger.LogInformation("Permissions seeding ended.");
    }

    private async Task<List<RoleConfig>> GetRolesSeedDataAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting reading roles...");

        var rolesJson = await File.ReadAllTextAsync(_accountsSeederOptions.RolesPath, cancellationToken);

        var rolesSeedData = JsonSerializer.Deserialize<List<RoleConfig>>
                                (rolesJson, JsonSerializerOptions.Default)
                            ?? throw new ApplicationException("Roles json is empty.");

        _logger.LogInformation("Roles reading ended.");

        return rolesSeedData;
    }

    private async Task SeedRolesAsync(
        IEnumerable<RoleConfig> rolesSeedData,
        RoleManager<Role> roleManager)
    {
        _logger.LogInformation("Starting seed roles.");

        foreach (var roleSeedData in rolesSeedData)
        {
            var isRoleExist = await roleManager.RoleExistsAsync(roleSeedData.Name);

            if (!isRoleExist)
                await roleManager.CreateAsync(new Role
                {
                    Name = roleSeedData.Name
                });
        }

        _logger.LogInformation("Roles seeding ended.");
    }

    private async Task<List<RolePermissionConfig>> GetRolePermissionsSeedDataAsync(
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting reading role permissions...");

        var rolePermissionJson =
            await File.ReadAllTextAsync(_accountsSeederOptions.RolePermissionsPath, cancellationToken);

        var rolePermissionsSeedData = JsonSerializer.Deserialize<List<RolePermissionConfig>>
                                          (rolePermissionJson, JsonSerializerOptions.Default)
                                      ?? throw new ApplicationException("Role permissions json is empty.");

        _logger.LogInformation("Role permissions reading ended.");

        return rolePermissionsSeedData;
    }

    private async Task SeedRolePermissionsAsync(
        IEnumerable<PermissionConfig> permissionsSeedData,
        IEnumerable<RoleConfig> rolesSeedData,
        IEnumerable<RolePermissionConfig> rolePermissionSeedData,
        RolePermissionManager rolePermissionManager,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting seed role permissions.");

        rolesSeedData = rolesSeedData.ToList();
        permissionsSeedData = permissionsSeedData.ToList();

        List<RolePermissionDto> rolePermissionsDtos = [];
        foreach (var rolePermission in rolePermissionSeedData)
        {
            var roleName = rolesSeedData.FirstOrDefault(
                r => r.Id == rolePermission.RoleId)!.Name;

            var permissionCode = permissionsSeedData.FirstOrDefault(
                p => p.Id == rolePermission.PermissionId)!.Code;

            var rolePermissionDto = new RolePermissionDto
            {
                RoleName = roleName,
                PermissionCode = permissionCode
            };

            rolePermissionsDtos.Add(rolePermissionDto);
        }

        await rolePermissionManager.AddRangeIfNotExistsAsync(rolePermissionsDtos, cancellationToken);

        _logger.LogInformation("Roles seeding ended.");
    }
}
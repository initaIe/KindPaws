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

// TODO: мб переделать на хостед сервис чтобы умирал после завершения работы
public class AccountsSeederService
{
    private readonly AccountsSeederOptions _accountsSeederOptions;
    private readonly PermissionManager _permissionManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly RolePermissionManager _rolePermissionManager;

    public AccountsSeederService(
        IOptions<AccountsSeederOptions> accountsSeederOptions,
        ILogger<AccountsSeederService> logger,
        PermissionManager permissionManager, 
        RoleManager<Role> roleManager, 
        RolePermissionManager rolePermissionManager)
    {
        _accountsSeederOptions = accountsSeederOptions.Value;
        _permissionManager = permissionManager;
        _roleManager = roleManager;
        _rolePermissionManager = rolePermissionManager;
    }

    public async Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        var permissionsSeedData = await GetPermissionsSeedDataAsync(cancellationToken);
        await SeedPermissionsAsync(
            permissionsSeedData,
            cancellationToken);

        var rolesSeedData = await GetRolesSeedDataAsync(cancellationToken);
        await SeedRolesAsync(
            rolesSeedData);

        var rolePermissionSeedData = await GetRolePermissionsSeedDataAsync(cancellationToken);
        await SeedRolePermissionsAsync(
            permissionsSeedData,
            rolesSeedData,
            rolePermissionSeedData,
            cancellationToken);
    }
    
    private async Task<List<PermissionConfig>> GetPermissionsSeedDataAsync(
        CancellationToken cancellationToken = default)
    {
        var permissionsJson = await File.ReadAllTextAsync(_accountsSeederOptions.PermissionsPath, cancellationToken);

        var permissionsSeedData = JsonSerializer.Deserialize<List<PermissionConfig>>
                                      (permissionsJson, JsonSerializerOptions.Default)
                                  ?? throw new ApplicationException("Permissions json is empty.");

        return permissionsSeedData;
    }

    private async Task SeedPermissionsAsync(
        IEnumerable<PermissionConfig> permissionsSeedData,
        CancellationToken cancellationToken = default)
    {
        var permissions = permissionsSeedData.Select(permissionSeedData => new Permission
        {
            Id = Guid.NewGuid(),
            Code = permissionSeedData.Code
        });

        await _permissionManager.AddRangeIfByCodeNotExistsAsync(permissions, cancellationToken);
    }

    private async Task<List<RoleConfig>> GetRolesSeedDataAsync(CancellationToken cancellationToken = default)
    {
        var rolesJson = await File.ReadAllTextAsync(_accountsSeederOptions.RolesPath, cancellationToken);

        var rolesSeedData = JsonSerializer.Deserialize<List<RoleConfig>>
                                (rolesJson, JsonSerializerOptions.Default)
                            ?? throw new ApplicationException("Roles json is empty.");

        return rolesSeedData;
    }

    private async Task SeedRolesAsync(IEnumerable<RoleConfig> rolesSeedData)
    {
        foreach (var roleSeedData in rolesSeedData)
        {
            var isRoleExist = await _roleManager.RoleExistsAsync(roleSeedData.Name);

            if (!isRoleExist)
                await _roleManager.CreateAsync(new Role
                {
                    Name = roleSeedData.Name
                });
        }
    }

    private async Task<List<RolePermissionConfig>> GetRolePermissionsSeedDataAsync(
        CancellationToken cancellationToken = default)
    {
        var rolePermissionJson =
            await File.ReadAllTextAsync(_accountsSeederOptions.RolePermissionsPath, cancellationToken);

        var rolePermissionsSeedData = JsonSerializer.Deserialize<List<RolePermissionConfig>>
                                          (rolePermissionJson, JsonSerializerOptions.Default)
                                      ?? throw new ApplicationException("Role permissions json is empty.");

        return rolePermissionsSeedData;
    }

    private async Task SeedRolePermissionsAsync(
        IEnumerable<PermissionConfig> permissionsSeedData,
        IEnumerable<RoleConfig> rolesSeedData,
        IEnumerable<RolePermissionConfig> rolePermissionSeedData,
        CancellationToken cancellationToken = default)
    {
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

        await _rolePermissionManager.AddRangeIfNotExistsAsync(rolePermissionsDtos, cancellationToken);
    }
}


public class AccountsSeeder
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<AccountsSeeder> _logger;

    public AccountsSeeder(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<AccountsSeeder> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting seed accounts...");

        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var seederService = scope.ServiceProvider.GetRequiredService<AccountsSeederService>();
        await seederService.ProcessAsync(cancellationToken);

        _logger.LogInformation("Accounts seeding ended.");
    }
}
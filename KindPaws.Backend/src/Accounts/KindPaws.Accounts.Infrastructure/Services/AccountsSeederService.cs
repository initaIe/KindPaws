using System.Text.Json;
using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Domain.Account;
using KindPaws.Accounts.Domain.Account.ValueObjectsManagement.ValueObjects;
using KindPaws.Accounts.Domain.Permission;
using KindPaws.Accounts.Domain.Role;
using KindPaws.Accounts.Infrastructure.Managers;
using KindPaws.Accounts.Infrastructure.Options;
using KindPaws.Accounts.Infrastructure.Seeding.Configs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace KindPaws.Accounts.Infrastructure.Services;

public class AccountsSeederService
{
    private readonly AccountsSeedingOptions _accountsSeedingOptions;
    private readonly PermissionManager _permissionManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly RolePermissionManager _rolePermissionManager;
    private readonly UserManager<Account> _userManager;

    public AccountsSeederService(
        IOptions<AccountsSeedingOptions> accountsSeederOptions,
        PermissionManager permissionManager,
        RoleManager<Role> roleManager,
        RolePermissionManager rolePermissionManager,
        UserManager<Account> userManager)
    {
        _accountsSeedingOptions = accountsSeederOptions.Value;
        _permissionManager = permissionManager;
        _roleManager = roleManager;
        _rolePermissionManager = rolePermissionManager;
        _userManager = userManager;
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

        await SeedAdminAsync(cancellationToken);
    }

    private async Task<List<PermissionConfig>> GetPermissionsSeedDataAsync(
        CancellationToken cancellationToken = default)
    {
        var permissionsJson = await File.ReadAllTextAsync(_accountsSeedingOptions.PermissionsPath, cancellationToken);

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
        var rolesJson = await File.ReadAllTextAsync(_accountsSeedingOptions.RolesPath, cancellationToken);

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
            await File.ReadAllTextAsync(_accountsSeedingOptions.RolePermissionsPath, cancellationToken);

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

    private async Task SeedAdminAsync(CancellationToken cancellationToken = default)
    {
        var adminRole = await _roleManager.FindByNameAsync(_accountsSeedingOptions.AdminCredentials.Role)
                        ?? throw new ApplicationException("Admin role was not found.");

        var adminByEmailExist = await _userManager.FindByEmailAsync(_accountsSeedingOptions.AdminCredentials.Email);
        if (adminByEmailExist != null)
            return;

        var userName = UserName.Create(_accountsSeedingOptions.AdminCredentials.UserName).Value;
        var email = EmailAddress.Create(_accountsSeedingOptions.AdminCredentials.Email).Value;

        var adminUser = Account.Create(
            userName,
            email);

        await _userManager.CreateAsync(adminUser, _accountsSeedingOptions.AdminCredentials.Password);
        await _userManager.AddToRoleAsync(adminUser, adminRole.Name!);
    }
}
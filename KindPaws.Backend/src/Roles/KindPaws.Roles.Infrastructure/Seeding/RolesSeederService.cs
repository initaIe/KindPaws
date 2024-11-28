using System.Text.Json;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Permissions.Contracts;
using KindPaws.Roles.Application.Helpers;
using KindPaws.Roles.Domain.AggregateRoot;
using KindPaws.Roles.Infrastructure.DbContexts;
using KindPaws.Roles.Infrastructure.Options;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KindPaws.Roles.Infrastructure.Seeding;

public class RolesSeederService
{
    private readonly ILogger<RolesSeederService> _logger;
    private readonly RolesSeederOptions _rolesSeederOptions;
    private readonly RolesWriteDbContext _rolesWriteDbContext;
    private readonly IPermissionsContract _permissionsContract;
    private readonly IUnitOfWork _unitOfWork;


    public RolesSeederService(
        ILogger<RolesSeederService> logger,
        IOptions<RolesSeederOptions> rolesSeederOptions,
        RolesWriteDbContext rolesWriteDbContext,
        [FromKeyedServices(Modules.Roles)] IUnitOfWork unitOfWork,
        IPermissionsContract permissionsContract)
    {
        _logger = logger;
        _rolesSeederOptions = rolesSeederOptions.Value;
        _rolesWriteDbContext = rolesWriteDbContext;
        _unitOfWork = unitOfWork;
        _permissionsContract = permissionsContract;
    }

    public async Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting roles seeding...");

        var roles = await GetRolesSeedDataAsync(cancellationToken);
        await SeedRolesAsync(roles, cancellationToken);

        var rolesPermissions = await GetRolesPermissionsSeedDataAsync(cancellationToken);
        await SeedRolesPermissionsAsync(roles, rolesPermissions, cancellationToken);

        _logger.LogInformation("Roles seeding was ended...");
    }

    private async Task<List<Role>> GetRolesSeedDataAsync(CancellationToken cancellationToken = default)
    {
        var rolesJson = await File.ReadAllTextAsync(
            _rolesSeederOptions.RolesPath,
            cancellationToken);

        var roleCodes =
            JsonSerializer.Deserialize<List<string>>(rolesJson,
                JsonSerializerOptions.Default) ?? throw new ApplicationException("Roles json is empty.");

        return roleCodes.Select(RoleHelper.ForceCreateNewRole).ToList();
    }

    private async Task SeedRolesAsync(
        IEnumerable<Role> roles,
        CancellationToken cancellationToken = default)
    {
        var existingRoleNames = await _rolesWriteDbContext.Roles
            .Select(r => r.Name)
            .ToListAsync(cancellationToken);

        var newRoles = roles
            .Where(r => !existingRoleNames.Contains(r.Name))
            .DistinctBy(r => r.Name)
            .ToList();

        if (newRoles.Count != 0)
        {
            await _rolesWriteDbContext.Roles.AddRangeAsync(newRoles, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task<Dictionary<string, string[]>> GetRolesPermissionsSeedDataAsync(
        CancellationToken cancellationToken = default)
    {
        var rolesPermissionsJson = await File.ReadAllTextAsync(
            _rolesSeederOptions.RolesPermissionsPath,
            cancellationToken);

        var rolePermissionsDictionary = JsonSerializer.Deserialize<Dictionary<string, string[]>>(rolesPermissionsJson)
                                        ?? throw new ApplicationException("RolePermissions json is empty.");

        return rolePermissionsDictionary;
    }

    private async Task SeedRolesPermissionsAsync(
        IEnumerable<Role> addedRoles,
        Dictionary<string, string[]> roleNamePermissions,
        CancellationToken cancellationToken = default)
    {
        addedRoles = addedRoles.ToList();

        foreach (var rolePermissions in roleNamePermissions)
        {
            var role = addedRoles.FirstOrDefault(role => role.Name.Value == rolePermissions.Key);
            if (role == null)
                throw new ApplicationException($"Seed role {rolePermissions.Key} does not exist.");

            List<Result<Guid, ErrorList>> permissionIdResults = [];
            foreach (var rolePermissionCode in rolePermissions.Value)
            {
                var permissionId = await _permissionsContract
                    .GetPermissionIdByCodeAsync(rolePermissionCode, cancellationToken);

                permissionIdResults.Add(permissionId);
            }

            if (permissionIdResults.Any(p => p.IsFailure))
                throw new ApplicationException($"RolePermissions seeding was failure.");
            
            var rolesPermissions = permissionIdResults
                .Select(p => PermissionId.Create(p.Value).Value);

            var newRolesPermissions = rolesPermissions
                .Where(rp => !role.Permissions.Contains(rp))
                .Distinct()
                .ToList();

            if (newRolesPermissions.Count != 0)
            {
                role.AddPermissions(newRolesPermissions);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
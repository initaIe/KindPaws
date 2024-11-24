using System.Text.Json;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Permissions.Application.Helpers;
using KindPaws.Permissions.Domain.AggregateRoot;
using KindPaws.Permissions.Infrastructure.DbContexts;
using KindPaws.Permissions.Infrastructure.Options;
using KindPaws.SharedKernel.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KindPaws.Permissions.Infrastructure.Seeding;

public class PermissionsSeederService
{
    private readonly ILogger<PermissionsSeederService> _logger;
    private readonly PermissionsSeederOptions _permissionsSeederOptions;
    private readonly PermissionsWriteDbContext _permissionsWriteDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public PermissionsSeederService(
        IOptions<PermissionsSeederOptions> permissionsSeederOptions,
        PermissionsWriteDbContext permissionsWriteDbContext,
        [FromKeyedServices(Modules.Permissions)]
        IUnitOfWork unitOfWork, 
        ILogger<PermissionsSeederService> logger)
    {
        _permissionsWriteDbContext = permissionsWriteDbContext;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _permissionsSeederOptions = permissionsSeederOptions.Value;
    }

    public async Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting permissions seeding...");
        
        var permissions = await GetPermissionsSeedDataAsync(cancellationToken);
        await SeedPermissionsAsync(permissions, cancellationToken);
        
        _logger.LogInformation("Permissions seeding was ended...");
    }

    private async Task<List<Permission>> GetPermissionsSeedDataAsync(
        CancellationToken cancellationToken = default)
    {
        var permissionsJson = await File.ReadAllTextAsync(
            _permissionsSeederOptions.PermissionsPath,
            cancellationToken);

        var permissionNames =
            JsonSerializer.Deserialize<List<string>>(permissionsJson,
                JsonSerializerOptions.Default) ?? throw new ApplicationException("Permissions json is empty.");

       return permissionNames.Select(PermissionHelper.ForceCreateNewPermission).ToList();
    }

    private async Task SeedPermissionsAsync(
        IEnumerable<Permission> permissions,
        CancellationToken cancellationToken = default)
    {
        var existingPermissionCodes = await _permissionsWriteDbContext.Permissions
            .Select(p => p.Code)
            .ToListAsync(cancellationToken);

        var newPermissions = permissions
            .Where(p => !existingPermissionCodes.Contains(p.Code))
            .DistinctBy(p=>p.Code)
            .ToList();

        if (newPermissions.Count != 0)
        {
            await _permissionsWriteDbContext.Permissions.AddRangeAsync(newPermissions, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
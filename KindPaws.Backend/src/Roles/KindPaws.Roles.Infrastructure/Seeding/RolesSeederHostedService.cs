using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KindPaws.Roles.Infrastructure.Seeding;

public class RolesSeederHostedService : BackgroundService
{
    private readonly ILogger<RolesSeederHostedService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RolesSeederHostedService(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<RolesSeederHostedService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Started RolesPermissionsSeederHostedService...");

        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var seeder = scope.ServiceProvider
            .GetRequiredService<RolesSeederService>();

        await seeder.ProcessAsync(stoppingToken);
    }
}
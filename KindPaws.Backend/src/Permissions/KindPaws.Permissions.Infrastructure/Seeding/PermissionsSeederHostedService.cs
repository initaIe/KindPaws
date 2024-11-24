using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KindPaws.Permissions.Infrastructure.Seeding;

public class PermissionsSeederHostedService : BackgroundService
{
    private readonly ILogger<PermissionsSeederHostedService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionsSeederHostedService(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<PermissionsSeederHostedService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Started PermissionsSeederHostedService...");

        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var seeder = scope.ServiceProvider
            .GetRequiredService<PermissionsSeederService>();

        await seeder.ProcessAsync(stoppingToken);
    }
}
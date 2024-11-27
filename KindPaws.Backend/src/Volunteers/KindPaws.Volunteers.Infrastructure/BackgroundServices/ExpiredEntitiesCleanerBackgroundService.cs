using KindPaws.Volunteers.Infrastructure.Options;
using KindPaws.Volunteers.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KindPaws.Volunteers.Infrastructure.BackgroundServices;

public class ExpiredEntitiesCleanerBackgroundService : BackgroundService
{
    private readonly ILogger<ExpiredEntitiesCleanerBackgroundService> _logger;
    private readonly IOptionsMonitor<ExpiredEntitiesCleanerBackgroundServiceOptions> _options;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ExpiredEntitiesCleanerBackgroundService(
        ILogger<ExpiredEntitiesCleanerBackgroundService> logger,
        IServiceScopeFactory serviceScopeFactory,
        IOptionsMonitor<ExpiredEntitiesCleanerBackgroundServiceOptions> options)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _options = options;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Started ExpiredEntitiesCleanerBackgroundService...");

        while (!stoppingToken.IsCancellationRequested)
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();
            var expiredEntitiesCleanerService =
                scope.ServiceProvider.GetRequiredService<ExpiredEntitiesCleanerService>();

            await expiredEntitiesCleanerService.ProcessAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromHours(_options.CurrentValue.IntervalInHours), stoppingToken);
        }
    }
}
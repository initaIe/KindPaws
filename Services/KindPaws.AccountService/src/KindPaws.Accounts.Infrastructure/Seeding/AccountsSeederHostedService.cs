using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KindPaws.Accounts.Infrastructure.Seeding;

public class AccountsSeederHostedService : BackgroundService
{
    private readonly ILogger<AccountsSeederHostedService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AccountsSeederHostedService(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<AccountsSeederHostedService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Started AccountsSeederHostedService...");

        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var seeder = scope.ServiceProvider
            .GetRequiredService<AccountsSeederService>();

        await seeder.ProcessAsync(stoppingToken);
    }
}
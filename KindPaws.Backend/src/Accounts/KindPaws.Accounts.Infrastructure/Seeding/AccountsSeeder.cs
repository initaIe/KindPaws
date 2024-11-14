using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Accounts.Infrastructure.Seeding;

// TODO: мб переделать на хостед сервис чтобы умирал после завершения работы

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
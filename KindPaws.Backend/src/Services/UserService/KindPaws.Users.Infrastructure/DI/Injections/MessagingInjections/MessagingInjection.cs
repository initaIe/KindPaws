using KindPaws.Core.Abstractions.IntegrationEvents;
using KindPaws.Core.Options;
using KindPaws.SharedKernel.DDD;
using KindPaws.Users.Infrastructure.Common.Options;
using KindPaws.Users.Infrastructure.DI.Injections.MessagingInjections.ConsumingInjections;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Users.Infrastructure.DI.Injections.MessagingInjections;

public static class MessagingInjection
{
    public static IServiceCollection AddMessaging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var typesToExclude = new List<Type>
        {
            typeof(IntegrationEvent),
            typeof(IIntegrationEvent),
            typeof(IEvent),
            typeof(INotification)
        };

        var rabbitmqOptions = configuration
            .GetRequiredSection(RabbitmqOptions.SectionName)
            .Get<RabbitmqOptions>()!;

        var accountsConsumingOptions = configuration
            .GetRequiredSection(AccountsConsumingOptions.SectionName)
            .Get<AccountsConsumingOptions>()!;

        services.AddAccountsConsuming(
            rabbitmqOptions,
            accountsConsumingOptions,
            typesToExclude);

        return services;
    }
}
using KindPaws.Auth.Infrastructure.Common.Options;
using KindPaws.Core.Abstractions.IntegrationEvents;
using KindPaws.Core.Options;
using KindPaws.SharedKernel.DDD;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI.Injections.MessagingInjections;

public static class MessagingInjection
{
    public static IServiceCollection AddMessaging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitmqOptions = configuration
            .GetRequiredSection(RabbitmqOptions.SectionName)
            .Get<RabbitmqOptions>()!;

        var typesToExclude = new List<Type>
        {
            typeof(IntegrationEvent),
            typeof(IIntegrationEvent),
            typeof(Event),
            typeof(IEvent),
            typeof(INotification)
        };

        var accountsMessagingOptions = configuration
            .GetRequiredSection(AccountsMessagingOptions.SectionName)
            .Get<AccountsMessagingOptions>()!;

        services.AddAccountsMessaging(
            rabbitmqOptions,
            accountsMessagingOptions,
            typesToExclude);

        return services;
    }
}
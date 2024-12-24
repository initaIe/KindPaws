using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Contracts.Messaging;
using KindPaws.Auth.Infrastructure.Common.Options;
using KindPaws.Core.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI.Injections.MessagingInjections;

public static class AccountsMessagingInjection
{
    public static IServiceCollection AddAccountsMessaging(
        this IServiceCollection services,
        RabbitmqOptions rabbitmqOptions,
        AccountsMessagingOptions accountsMessagingOptions,
        List<Type> typesToExclude)
    {
        services.AddAccountsMessageBus(
            rabbitmqOptions,
            accountsMessagingOptions,
            typesToExclude);

        return services;
    }

    private static IServiceCollection AddAccountsMessageBus(
        this IServiceCollection services,
        RabbitmqOptions rabbitmqOptions,
        AccountsMessagingOptions accountsMessagingOptions,
        List<Type> typesToExclude)
    {
        services.AddMassTransit<IAccountsMessageBus>(configurator =>
        {
            configurator.SetKebabCaseEndpointNameFormatter();

            configurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureHost(rabbitmqOptions);
                cfg.ConfigureMessagesToExchanges(accountsMessagingOptions);
                cfg.ConfigureExchanges(accountsMessagingOptions);
                cfg.ConfigureRoutingKeys();
                cfg.ConfigureExcludingUnnecessaryTypes(typesToExclude);
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }

    private static void ConfigureHost(
        this IRabbitMqBusFactoryConfigurator cfg,
        RabbitmqOptions rabbitmqOptions)
    {
        cfg.Host(new Uri(rabbitmqOptions.Host), h =>
        {
            h.Username(rabbitmqOptions.Username);
            h.Password(rabbitmqOptions.Password);
        });
    }

    private static void ConfigureExcludingUnnecessaryTypes(
        this IRabbitMqBusFactoryConfigurator cfg,
        List<Type> typesToExclude)
    {
        typesToExclude.ForEach(typeToExclude =>
            cfg.Publish(typeToExclude, configurator => configurator.Exclude = true));
    }

    private static void ConfigureMessagesToExchanges(
        this IRabbitMqBusFactoryConfigurator cfg,
        AccountsMessagingOptions accountsMessagingOptions)
    {
        cfg.Message<AccountCreatedIntegrationEvent>(configurator =>
            configurator.SetEntityName(accountsMessagingOptions.ExchangeName));
    }

    private static void ConfigureRoutingKeys(this IRabbitMqBusFactoryConfigurator cfg)
    {
        cfg.Send<AccountCreatedIntegrationEvent>(configurator =>
            configurator.UseRoutingKeyFormatter(context => AccountCreatedIntegrationEvent.RoutingKey)
        );
    }

    private static void ConfigureExchanges(
        this IRabbitMqBusFactoryConfigurator cfg,
        AccountsMessagingOptions accountsMessagingOptions)
    {
        cfg.Publish<AccountCreatedIntegrationEvent>(configurator =>
        {
            configurator.ExchangeType = accountsMessagingOptions.ExchangeType;
            configurator.Durable = accountsMessagingOptions.ExchangeDurable;
            configurator.AutoDelete = accountsMessagingOptions.ExchangeAutoDelete;
        });
    }
}
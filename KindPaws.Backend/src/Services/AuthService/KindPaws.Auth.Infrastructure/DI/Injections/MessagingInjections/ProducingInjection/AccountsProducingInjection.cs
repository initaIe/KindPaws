using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Contracts.Messaging;
using KindPaws.Auth.Infrastructure.Common.Options;
using KindPaws.Core.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI.Injections.MessagingInjections.ProducingInjection;

public static class AccountsProducingInjection
{
    public static IServiceCollection AddAccountsProducing(
        this IServiceCollection services,
        RabbitmqOptions rabbitmqOptions,
        AccountsProducingOptions accountsProducingOptions,
        List<Type> typesToExclude)
    {
        services.AddAccountProducers(
            rabbitmqOptions,
            accountsProducingOptions,
            typesToExclude);

        return services;
    }

    private static IServiceCollection AddAccountProducers(
        this IServiceCollection services,
        RabbitmqOptions rabbitmqOptions,
        AccountsProducingOptions accountsProducingOptions,
        List<Type> typesToExclude)
    {
        services.AddMassTransit<IAccountsMessageBus>(configurator =>
        {
            configurator.SetKebabCaseEndpointNameFormatter();

            configurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureHost(rabbitmqOptions);
                cfg.ConfigureExchange(accountsProducingOptions);
                cfg.ConfigureRoutingKeys();
                cfg.ExcludeUnnecessaryTypes(typesToExclude);
            });
        });
        return services;
    }

    private static void ConfigureHost(
        this IRabbitMqBusFactoryConfigurator cfg,
        RabbitmqOptions rabbitmqOptions)
    {
        cfg.Host(new Uri(rabbitmqOptions.Host), configurator =>
        {
            configurator.Username(rabbitmqOptions.Username);
            configurator.Password(rabbitmqOptions.Password);
        });
    }

    private static void ExcludeUnnecessaryTypes(
        this IRabbitMqBusFactoryConfigurator cfg,
        List<Type> typesToExclude)
    {
        typesToExclude.ForEach(typeToExclude =>
            cfg.Publish(typeToExclude, configurator => configurator.Exclude = true));
    }

    private static void ConfigureRoutingKeys(this IRabbitMqBusFactoryConfigurator cfg)
    {
        cfg.Send<AccountCreatedIntegrationEvent>(topologyConfigurator =>
            topologyConfigurator.UseRoutingKeyFormatter(context => AccountCreatedIntegrationEvent.RoutingKey)
        );
    }

    private static void ConfigureExchange(
        this IRabbitMqBusFactoryConfigurator cfg,
        AccountsProducingOptions accountsProducingOptions)
    {
        cfg.Message<AccountCreatedIntegrationEvent>(configurator =>
            configurator.SetEntityName(accountsProducingOptions.ExchangeName));

        cfg.Publish<AccountCreatedIntegrationEvent>(configurator =>
        {
            configurator.ExchangeType = accountsProducingOptions.ExchangeType;
            configurator.Durable = accountsProducingOptions.ExchangeDurable;
            configurator.AutoDelete = accountsProducingOptions.ExchangeAutoDelete;
        });
    }
}
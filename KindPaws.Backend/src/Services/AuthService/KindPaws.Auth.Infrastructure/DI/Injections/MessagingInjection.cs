using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Application.Features.Accounts.EventHandlers;
using KindPaws.Auth.Contracts.Messaging;
using KindPaws.Auth.Infrastructure.Options;
using KindPaws.Core.Abstractions.IntegrationEvents;
using KindPaws.SharedKernel.DDD;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Event = KindPaws.SharedKernel.DDD.Event;

namespace KindPaws.Auth.Infrastructure.DI.Injections;

public static class MessagingInjection
{
    public static IServiceCollection AddMessaging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddRabbitmq(configuration);

        return services;
    }

    private static IServiceCollection AddRabbitmq(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit<IAuthMessageBus>(configurator =>
        {
            configurator.SetKebabCaseEndpointNameFormatter();
            
            configurator.UsingRabbitMq((context, cfg) =>
            {
                var rabbitmqOptions = configuration
                    .GetRequiredSection(RabbitmqOptions.SectionName)
                    .Get<RabbitmqOptions>()!;

                cfg.ConfigureHost(rabbitmqOptions);
                cfg.MapMessagesToExchanges(rabbitmqOptions);
                cfg.ConfigureExchange(rabbitmqOptions);
                cfg.ConfigureRoutingKeys();
                cfg.ExcludeUnnecessaryTypes();
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

    private static void ExcludeUnnecessaryTypes(this IRabbitMqBusFactoryConfigurator cfg)
    {
        var typesToExclude = new List<Type>
        {
            typeof(IntegrationEvent),
            typeof(IIntegrationEvent),
            typeof(Event),
            typeof(IEvent),
            typeof(INotification)
        };

        typesToExclude.ForEach(typeToExclude =>
            cfg.Publish(typeToExclude, configurator => configurator.Exclude = true));
    }

    private static void MapMessagesToExchanges(
        this IRabbitMqBusFactoryConfigurator cfg,
        RabbitmqOptions rabbitmqOptions)
    {
        cfg.Message<AccountCreatedIntegrationEvent>(configurator =>
            configurator.SetEntityName(rabbitmqOptions.ExchangeName));
    }

    private static void ConfigureRoutingKeys(this IRabbitMqBusFactoryConfigurator cfg)
    {
        cfg.Send<AccountCreatedIntegrationEvent>(configurator =>
            configurator.UseRoutingKeyFormatter(context => "account.created")
        );
    }

    private static void ConfigureExchange(
        this IRabbitMqBusFactoryConfigurator cfg,
        RabbitmqOptions rabbitmqOptions)
    {
        cfg.Publish<AccountCreatedIntegrationEvent>(configurator =>
        {
            configurator.ExchangeType = rabbitmqOptions.ExchangeType;
            configurator.Durable = rabbitmqOptions.ExchangeDurable;
            configurator.AutoDelete = rabbitmqOptions.ExchangeAutoDelete;
        });
    }
}
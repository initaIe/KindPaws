using KindPaws.Auth.Contracts.Messaging;
using KindPaws.Core.Options;
using KindPaws.Users.Infrastructure.Common;
using KindPaws.Users.Infrastructure.Common.Options;
using KindPaws.Users.Infrastructure.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Users.Infrastructure.DI.Injections.MessagingInjections.ConsumingInjections;

public static class AccountConsumersInjection
{
    public static IServiceCollection AddAccountsConsuming(
        this IServiceCollection services,
        RabbitmqOptions rabbitmqOptions,
        AccountsConsumingOptions accountsConsumingOptions,
        List<Type> typesToExclude)
    {
        services.AddAccountConsumers(
            rabbitmqOptions,
            accountsConsumingOptions,
            typesToExclude);

        return services;
    }

    private static IServiceCollection AddAccountConsumers(
        this IServiceCollection services,
        RabbitmqOptions rabbitmqOptions,
        AccountsConsumingOptions accountsConsumingOptions,
        List<Type> typesToExclude)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumers(typeof(UsersInfrastructureAssemblyReference).Assembly);

            configurator.SetKebabCaseEndpointNameFormatter();

            configurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureHost(rabbitmqOptions);
                cfg.ConfigureConsumers(context, accountsConsumingOptions);
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

    private static void ConfigureConsumers(
        this IRabbitMqBusFactoryConfigurator cfg,
        IBusRegistrationContext context,
        AccountsConsumingOptions accountsConsumingOptions)
    {
        cfg.ReceiveEndpoint(AccountCreatedIntegrationEvent.QueueName, endpoint =>
        {
            endpoint.ConfigureConsumeTopology = false;
            endpoint.Bind(accountsConsumingOptions.ExchangeName, x =>
            {
                x.RoutingKey = AccountCreatedIntegrationEvent.RoutingKey;
                x.ExchangeType = accountsConsumingOptions.ExchangeType;
                x.Durable = accountsConsumingOptions.ExchangeDurable;
                x.AutoDelete = accountsConsumingOptions.ExchangeAutoDelete;
            });
            endpoint.ConfigureConsumer<AccountCreatedIntegrationEventConsumer>(context);
        });
    }
}
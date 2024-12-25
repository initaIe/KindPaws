using KindPaws.Core.MessageBox.Abstractions.Interfaces;
using KindPaws.Core.MessageBox.Schedulers;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace KindPaws.Pets.Infrastructure.DI.Injections;

public static class SchedulersInjection
{
    public static IServiceCollection AddSchedulers(this IServiceCollection services)
    {
        services.AddOutBoxMessagePublisherJob();

        return services;
    }

    private static IServiceCollection AddOutBoxMessagePublisherJob(this IServiceCollection services)
    {
        services.AddScoped<IOutboxMessagePublisherService, OutboxMessagePublisherService>();

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(OutboxMessagePublisherJob));

            configure
                .AddJob<OutboxMessagePublisherJob>(jobKey)
                .AddTrigger(trigger => trigger.ForJob(jobKey)
                    .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(1).RepeatForever()));
        });

        services.AddQuartzHostedService(options => { options.WaitForJobsToComplete = true; });

        return services;
    }
}
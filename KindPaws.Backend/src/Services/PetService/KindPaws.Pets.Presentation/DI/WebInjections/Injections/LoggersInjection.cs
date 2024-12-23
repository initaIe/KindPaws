using KindPaws.Core.Options;
using Serilog;
using Serilog.Events;

namespace KindPaws.Pets.Presentation.DI.WebInjections.Injections;

public static class LoggersInjection
{
    public static IServiceCollection AddSerilogLogger(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var seqOptions = configuration.GetRequiredSection(SeqOptions.SectionName).Get<SeqOptions>()!;

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Seq(seqOptions.ConnectionString)
            .MinimumLevel.Override(seqOptions.AspNetCoreMinimumLevel,
                (LogEventLevel)Enum.Parse(typeof(LogEventLevel), seqOptions.LogEventLevel))
            .CreateLogger();

        services.AddSerilog();

        return services;
    }
}
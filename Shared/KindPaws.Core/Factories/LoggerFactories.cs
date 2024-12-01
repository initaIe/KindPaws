using Microsoft.Extensions.Logging;

namespace KindPaws.Core.Factories;

public static class LoggerFactories
{
    public static ILoggerFactory CreateConsole()
    {
        return LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MonopolyKata;

// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1#non-host-console-app
public static class LoggingExtensions
{
    public static IServiceCollection AddMonopolyLogging(this IServiceCollection services)
    {
        return services.AddLogging(builder => builder
            .SetMinimumLevel(LogLevel.Debug)
            .AddProvider(new ColoredConsoleLoggerProvider(
                new ColoredConsoleLoggerConfiguration
                {
                    LogLevel = LogLevel.Information,
                    Color = ConsoleColor.Gray
                })
            )
            .AddProvider(new ColoredConsoleLoggerProvider(
                new ColoredConsoleLoggerConfiguration
                {
                    LogLevel = LogLevel.Warning,
                    Color = ConsoleColor.Red
                })
            )
            .AddProvider(new ColoredConsoleLoggerProvider(
                new ColoredConsoleLoggerConfiguration
                {
                    LogLevel = LogLevel.Debug,
                    Color = ConsoleColor.Yellow
                })
            )
        );
    }
}
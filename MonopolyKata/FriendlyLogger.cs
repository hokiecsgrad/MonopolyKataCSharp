using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace MonopolyKata
{
    public class ColoredConsoleLoggerConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
        public int EventId { get; set; } = 0;
        public ConsoleColor Color { get; set; } = ConsoleColor.Gray;
    }

    public class ColoredConsoleLoggerProvider(ColoredConsoleLoggerConfiguration config) : ILoggerProvider
    {
        private readonly ColoredConsoleLoggerConfiguration _config = config;
        private readonly ConcurrentDictionary<string, ColoredConsoleLogger> _loggers = new();

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new ColoredConsoleLogger(name, _config));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }

    public static class ColoredConsoleLoggerExtensions
    {
        public static ILoggerFactory AddColoredConsoleLogger(this ILoggerFactory loggerFactory, ColoredConsoleLoggerConfiguration config)
        {
            loggerFactory.AddProvider(new ColoredConsoleLoggerProvider(config));
            return loggerFactory;
        }
        public static ILoggerFactory AddColoredConsoleLogger(this ILoggerFactory loggerFactory)
        {
            var config = new ColoredConsoleLoggerConfiguration();
            return loggerFactory.AddColoredConsoleLogger(config);
        }
        public static ILoggerFactory AddColoredConsoleLogger(this ILoggerFactory loggerFactory, Action<ColoredConsoleLoggerConfiguration> configure)
        {
            var config = new ColoredConsoleLoggerConfiguration();
            configure(config);
            return loggerFactory.AddColoredConsoleLogger(config);
        }
    }

    public class ColoredConsoleLogger : ILogger
    {
        private readonly string _name;
        private readonly ColoredConsoleLoggerConfiguration _config;

        public ColoredConsoleLogger(string name, ColoredConsoleLoggerConfiguration config)
        {
            _name = name;
            _config = config;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return new NoOpDisposable();
        }

        private class NoOpDisposable : IDisposable
        {
            public void Dispose() { }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (_config.EventId == 0 || _config.EventId == eventId.Id)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = _config.Color;
                Console.WriteLine($"{formatter(state, exception)}");
                Console.ForegroundColor = color;
            }
        }
    }
}
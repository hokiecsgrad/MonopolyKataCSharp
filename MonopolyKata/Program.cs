using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MonopolyKata
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://andrewlock.net/using-dependency-injection-in-a-net-core-console-application/
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1#non-host-console-app
            var serviceProvider = new ServiceCollection()
                .AddLogging(
                    builder => 
                        builder
                            .SetMinimumLevel(LogLevel.Debug)
                            .AddProvider(new ColoredConsoleLoggerProvider(
                                new ColoredConsoleLoggerConfiguration
                                    {
                                        LogLevel = LogLevel.Information,
                                        Color = ConsoleColor.Yellow
                                    })
                                )
                            .AddProvider(new ColoredConsoleLoggerProvider(
                                new ColoredConsoleLoggerConfiguration
                                    {
                                        LogLevel = LogLevel.Warning,
                                        Color = ConsoleColor.Red
                                    })
                                )
                    )
                .AddSingleton<IBoard, MonopolyBoard>()
                .AddSingleton<IDice, Dice>()
                .AddSingleton<ITurn, Turn>()
                .AddScoped<Monopoly>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogDebug("Starting Application");

            Player ryan = new Player("Ryan");
            Player computer = new Player("Computer");
            List<Player> players = new List<Player> { ryan, computer };
            Monopoly game = serviceProvider.GetService<Monopoly>();

            ryan.Bank = 1500;
            computer.Bank = 1500;
            game.AddPlayer(ryan);
            game.AddPlayer(computer);
            game.Start();

            logger.LogDebug("Ending Application");
        }
    }
}

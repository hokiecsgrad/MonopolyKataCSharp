﻿using System;
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
                    )
                .AddSingleton<IBoard, MonopolyBoard>()
                .AddSingleton<IDice, Dice>()
                .AddSingleton<ITurn, Turn>()
                .AddScoped<Monopoly>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogDebug("Starting Application");


            Monopoly game = serviceProvider.GetService<Monopoly>();

            Player ryan = new Player("Ryan", serviceProvider.GetService<ILoggerFactory>());
            Player cyndi = new Player("Cyndi", serviceProvider.GetService<ILoggerFactory>());
            Player bo = new Player("Bo", serviceProvider.GetService<ILoggerFactory>());
            Player cinder = new Player("Cinder", serviceProvider.GetService<ILoggerFactory>());
            Player fiona = new Player("Fiona", serviceProvider.GetService<ILoggerFactory>());
            List<Player> players = new List<Player> { ryan, cyndi, bo, cinder, fiona };

            foreach (Player player in players)
            {
                player.Bank = 1500;
                game.AddPlayer(player);
            }

            game.Start();
            

            logger.LogDebug("Ending Application");
        }
    }
}

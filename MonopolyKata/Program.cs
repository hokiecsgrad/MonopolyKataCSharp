using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using MonopolyKata.Strategies;

namespace MonopolyKata;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                // Clean up the default logging format
                logging.ClearProviders();
                logging.AddConsole();

                // Make it look nicer
                logging.AddSimpleConsole(options =>
                {
                    options.SingleLine = true;
                    options.TimestampFormat = "[HH:mm:ss] ";
                    options.ColorBehavior = LoggerColorBehavior.Enabled;
                });
            })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IBoard, MonopolyBoard>();
                services.AddSingleton<IDice, Dice>();
                services.AddSingleton<ITurn, Turn>();
                services.AddSingleton<IPlayerFactory, PlayerFactory>();

                services.AddSingleton<IPlayerPersonality, CapitalistPersonality>();
                services.AddSingleton<IPlayerPersonality, SocialistPersonality>();
                services.AddSingleton<IPlayerPersonality, GamblerPersonality>();
                services.AddSingleton<IPlayerPersonality, MiserPersonality>();
                services.AddSingleton<IPlayerPersonality, RailroadTycoonPersonality>();

                services.AddSingleton<Monopoly>();

                services.Configure<MonopolySettings>(
                    context.Configuration.GetSection("MonopolySettings")
                    );

                services.AddHostedService<MonopolyLogger>();
                services.AddHostedService<MonopolyHostedService>();
            })
            .Build();

        await host.RunAsync();
    }
}

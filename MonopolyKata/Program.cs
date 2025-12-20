using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace MonopolyKata
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // https://andrewlock.net/using-dependency-injection-in-a-net-core-console-application/
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    // This removes the default "noisy" loggers (Console, Debug, EventSource)
                    logging.ClearProviders();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddMonopolyLogging();

                    services.AddSingleton<IBoard, MonopolyBoard>();
                    services.AddSingleton<IDice, Dice>();
                    services.AddSingleton<ITurn, Turn>();
                    services.AddSingleton<IPlayerFactory, PlayerFactory>();

                    services.AddSingleton<Monopoly>();

                    // This binds the "MonopolySettings" section of JSON to the C# class
                    services.Configure<MonopolySettings>(
                        context.Configuration.GetSection("MonopolySettings")
                        );

                    // This tells the Host: "When you start, run this class in the background."
                    services.AddHostedService<MonopolyHostedService>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}

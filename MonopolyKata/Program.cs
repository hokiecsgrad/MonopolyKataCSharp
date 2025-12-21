using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace MonopolyKata
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    // Clean up the default logging format
                    logging.ClearProviders();
                    logging.AddConsole(); // Add the standard logger back

                    // Optional: Make it look nicer (single line, no timestamps)
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

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace MonopolyKata
{
    // Inheriting from BackgroundService handles the Start/Stop wiring for us
    public class MonopolyHostedService : BackgroundService
    {
        private readonly Monopoly _game;
        private readonly IPlayerFactory _playerFactory;
        private readonly ILogger<MonopolyHostedService> _logger;
        private readonly IHostApplicationLifetime _appLifetime;

        public MonopolyHostedService(
            Monopoly game,
            IPlayerFactory playerFactory,
            ILogger<MonopolyHostedService> logger,
            IHostApplicationLifetime appLifetime)
        {
            _game = game;
            _playerFactory = playerFactory;
            _logger = logger;
            _appLifetime = appLifetime;
        }

        // This is the entry point. The Host calls this automatically when it starts.
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Monopoly Service Started");

            try
            {
                // Setup the game
                var playerNames = new[] { "Ryan", "Cyndi", "Bo", "Cinder", "Fiona" };

                foreach (var name in playerNames)
                {
                    // If the user hits Ctrl+C, stoppingToken triggers. 
                    // We check it to exit loops gracefully.
                    if (stoppingToken.IsCancellationRequested) break;

                    var player = _playerFactory.Create(name);
                    player.Bank = 1500;
                    _game.AddPlayer(player);
                }

                // Run the game logic
                // (Assuming Game.Start is synchronous, we wrap it purely for this example,
                // but usually, you'd want game logic to be async if it involves IO)
                _logger.LogInformation("Game is starting...");

                _game.Start();

                _logger.LogInformation("Game Over!");
            }
            finally
            {
                // CRITICAL: This line tells the Generic Host "My work is done, please exit."
                // Without this, the console window will stay open forever waiting for Ctrl+C.
                _appLifetime.StopApplication();
            }
        }
    }
}
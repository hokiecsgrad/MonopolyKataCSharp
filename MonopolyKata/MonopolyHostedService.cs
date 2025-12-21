using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly MonopolySettings _settings;
        private readonly IHostApplicationLifetime _appLifetime;

        public MonopolyHostedService(
            Monopoly game,
            IPlayerFactory playerFactory,
            IOptions<MonopolySettings> options,
            ILogger<MonopolyHostedService> logger,
            IHostApplicationLifetime appLifetime)
        {
            _game = game;
            _playerFactory = playerFactory;
            _settings = options.Value;
            _logger = logger;
            _appLifetime = appLifetime;
        }

        // This is the entry point. The Host calls this automatically when it starts.
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Monopoly Service Started");

            try
            {
                foreach (var playerSetting in _settings.Players)
                {
                    var player = _playerFactory.Create(playerSetting.Name);
                    player.Bank = _settings.StartingBalance;
                    _game.AddPlayer(player);
                }

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
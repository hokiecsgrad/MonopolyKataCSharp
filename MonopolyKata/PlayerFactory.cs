using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MonopolyKata.Strategies;

namespace MonopolyKata;

public class PlayerFactory : IPlayerFactory
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly MonopolySettings _settings;
    private readonly IEnumerable<IPlayerPersonality> _personalities;

    // The container automatically provides the loggerFactory here
    public PlayerFactory(
        ILoggerFactory loggerFactory,
        IOptions<MonopolySettings> options,
        IEnumerable<IPlayerPersonality> personalities
        )
    {
        _loggerFactory = loggerFactory;
        _settings = options.Value;
        _personalities = personalities;
    }

    public Player Create(string name)
    {
        var playerConfig = _settings.Players.FirstOrDefault(p => p.Name == name);

        // Default to "Capitalist" if not found or not specified
        string targetPersonality = playerConfig?.Personality ?? "Capitalist";

        var strategy = _personalities.FirstOrDefault(s =>
            s.GetType().Name.StartsWith(targetPersonality, StringComparison.OrdinalIgnoreCase));

        if (strategy == null)
            strategy = _personalities.OfType<CapitalistPersonality>().First();

        var logger = _loggerFactory.CreateLogger<Player>();

        return new Player(name, strategy, logger);
    }
}
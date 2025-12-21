using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MonopolyKata;

public class Monopoly(
    IBoard? board,
    ITurn? turn,
    IOptions<MonopolySettings> settings,
    ILoggerFactory? loggerFactory = null
    )
{
    private readonly ILogger<Monopoly>? _logger = loggerFactory?.CreateLogger<Monopoly>();
    private IBoard? Board { get; } = board;
    private ITurn? Turn { get; } = turn;

    private int MaxRounds = settings.Value.MaxRounds;
    private readonly Random RandomGenerator = new();

    public List<Player> Players { get; private set; } = [];
    public int Rounds = 0;

    public void AddPlayer(Player player)
    {
        Players.Add(player);
        player.GameRef = this;
    }

    public void Start()
    {
        SetupGame();
        PlayGame();
    }

    private void SetupGame()
    {
        ValidatePlayers();
        RandomizePlayerOrder();
        AddPlayersToStartingPosition();
    }

    private void ValidatePlayers()
    {
        if (Players.Count < 2 || Players.Count > 8)
            throw new InvalidOperationException("You must have at least 2 players and no more than 8 players!");
    }

    private void RandomizePlayerOrder()
    {
        Players = Players?.OrderBy(a => RandomGenerator.Next()).ToList() ?? [];
    }

    private void AddPlayersToStartingPosition()
    {
        foreach (Player player in Players)
            Board?.AddPlayerToBoard(player, 0);
    }

    private void PlayGame()
    {
        while (Players.Count > 1 && Rounds < MaxRounds)
            PlayRound();

        Player winner = GetWinner();

        _logger?.LogInformation("");
        _logger?.LogInformation($"The players played a total of {Rounds} rounds.");
        _logger?.LogInformation($"{winner.Name} wins the game with ${winner.Bank} in the bank!");
    }

    private void PlayRound()
    {
        _logger?.LogDebug("");
        _logger?.LogDebug($"Starting round # {Rounds}.");

        foreach (Player player in Players)
        {
            Turn?.Take(player);
            player.Rounds++;
        }

        RemoveBankruptPlayers();

        Rounds++;
    }

    private void RemoveBankruptPlayers()
    {
        List<Player> bankruptedPlayers = [.. Players.FindAll(player => player.Bank < 0)];
        foreach (Player loser in bankruptedPlayers)
        {
            loser.Bankrupt();
            Players.Remove(loser);
            _logger?.LogInformation("");
            _logger?.LogInformation($"{loser.Name} has gone bankrupt and is now out of the game.");
        }
    }

    public Player GetWinner()
    {
        return Players.OrderByDescending(player => player.Bank).First();
    }
}

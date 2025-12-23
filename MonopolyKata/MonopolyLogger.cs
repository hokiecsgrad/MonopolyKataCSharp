using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

using MonopolyKata.Spaces;

namespace MonopolyKata;

public class MonopolyLogger : IHostedService
{
    private readonly IBoard _board;
    private readonly Monopoly _game;
    private readonly ILogger<MonopolyLogger> _logger;

    public MonopolyLogger(IBoard board, Monopoly game, ILogger<MonopolyLogger> logger)
    {
        _board = board;
        _game = game;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken token)
    {
        // Subscribe to every property on the board
        foreach (var property in _board.Spaces.OfType<Property>())
        {
            property.OnPropertyBought += LogPurchase;
            property.OnRentPaid += LogRent;
            property.OnPropertyReset += LogReset;
        }

        _game.PlayerAdded += SubscribeToNewPlayer;

        return Task.CompletedTask;
    }

    private void SubscribeToNewPlayer(object? sender, Player player)
    {
        // This runs every time game.AddPlayer() is called.
        // NOW we can hook into the specific player's events.
        player.BankBalanceChanged += LogBankBalance;
        player.PlayerWentBankrupt += LogBankruptcy;
    }

    private void LogPurchase(object? sender, Player player)
    {
        if (sender is Property property)
        {
            _logger.LogInformation("{Player} bought {Property} for ${Amount}",
                player.Name, property.Name, property.PurchasePrice);
        }
    }

    private void LogRent(object? sender, Player player)
    {
        if (sender is Property property)
        {
            _logger.LogInformation("{Player} had to pay rent to {Owner}.",
                player.Name, property.Owner.Name);
        }
    }

    private void LogReset(object? sender, Property prop)
    {
        if (sender is Property property)
        {
            _logger.LogInformation("{Property} had to be sold to the bank.",
                property.Name);
        }
    }

    private void LogBankBalance(object? sender, int balance)
    {
        if (sender is Player player)
        {
            _logger?.LogInformation($"{player.Name}'s bank balance is now ${balance}.");
        }
    }

    private void LogBankruptcy(object? sender, EventArgs e)
    {
        if (sender is Player player)
        {
            _logger.LogWarning("GAME OVER for {Name}! They have gone bankrupt.", player.Name);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Unsubscribe when the app shuts down to prevent memory leaks
        // (Though less critical in a console app that's about to exit anyway)
        foreach (var property in _board.Spaces.OfType<Property>())
        {
            property.OnPropertyBought -= LogPurchase;
            property.OnRentPaid -= LogRent;
            property.OnPropertyReset -= LogReset;
        }

        _game.PlayerAdded -= SubscribeToNewPlayer;

        return Task.CompletedTask;
    }
}
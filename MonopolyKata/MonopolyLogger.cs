using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

using MonopolyKata.Spaces;

namespace MonopolyKata;

public class MonopolyLogger : IHostedService
{
    private readonly IBoard _board;
    private readonly ILogger<MonopolyLogger> _logger;

    public MonopolyLogger(IBoard board, ILogger<MonopolyLogger> logger)
    {
        _board = board;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken token)
    {
        // Subscribe to every property on the board
        foreach (var space in _board.Spaces.OfType<Property>())
        {
            space.OnPropertyBought += LogPurchase;
            space.OnRentPaid += LogRent;
        }
        return Task.CompletedTask;
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

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Unsubscribe when the app shuts down to prevent memory leaks
        // (Though less critical in a console app that's about to exit anyway)
        foreach (var property in _board.Spaces.OfType<Property>())
        {
            property.OnPropertyBought -= LogPurchase;
            property.OnRentPaid -= LogRent;
        }

        return Task.CompletedTask;
    }
}
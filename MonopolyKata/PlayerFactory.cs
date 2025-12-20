using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MonopolyKata;

public class PlayerFactory : IPlayerFactory
{
    private readonly ILoggerFactory _loggerFactory;

    // The container automatically provides the loggerFactory here
    public PlayerFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    public Player Create(string name)
    {
        // You provide the name, the factory provides the logger
        return new Player(name, _loggerFactory);
    }
}
using MonopolyKata;
using MonopolyKata.Spaces;

namespace MonopolyKata.Strategies;

public interface IPlayerPersonality
{
    // Returns true if the player WANTS the property. 
    // We handle "Can I afford it?" separately in the Property class.
    bool ShouldBuy(Player player, Property property);
}

// 1. The Default: Buy everything in sight
public class CapitalistPersonality : IPlayerPersonality
{
    public bool ShouldBuy(Player player, Property property)
    {
        return true; // "If it's for sale, I want it."
    }
}

// 2. The Socialist: Prevent Monopolies
public class SocialistPersonality : IPlayerPersonality
{
    public bool ShouldBuy(Player player, Property property)
    {
        // Logic: Do I already own a property of this color?
        bool ownsColorGroup = player.Properties
            .Any(p => p.Group == property.Group);

        // If I own one, I stop buying to prevent a monopoly.
        // If I don't own one, I buy it to block others.
        return !ownsColorGroup;
    }
}

public class GamblerPersonality : IPlayerPersonality
{
    private readonly Random _random = new Random();

    public bool ShouldBuy(Player player, Property property)
    {
        // 50/50 chance to buy or pass
        return _random.NextDouble() >= 0.5;
    }
}

public class MiserPersonality : IPlayerPersonality
{
    private const int SafetyBuffer = 500;

    public bool ShouldBuy(Player player, Property property)
    {
        // Only buy if I will still have $500 left after the purchase
        return (player.Bank - property.PurchasePrice) >= SafetyBuffer;
    }
}

public class RailroadTycoonPersonality : IPlayerPersonality
{
    public bool ShouldBuy(Player player, Property property)
    {
        // Ideally, check against an Enum or Const, but string matching works for now
        if (property.Group.Name.Contains("Railroad") || property.Group.Name.Contains("Utility"))
        {
            return true;
        }

        // Also buy the cheap properties (Brown/LightBlue) for quick hotels
        if (property.PurchasePrice < 150)
        {
            return true;
        }

        return false;
    }
}

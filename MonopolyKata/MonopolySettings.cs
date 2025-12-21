
namespace MonopolyKata;

public class MonopolySettings
{
    public int MaxRounds { get; set; }
    public int StartingBalance { get; set; }
    public List<PlayerConfig> Players { get; set; } // Changed from string[] PlayerNames
}

public class PlayerConfig
{
    public string Name { get; set; }
    public string Personality { get; set; }
}
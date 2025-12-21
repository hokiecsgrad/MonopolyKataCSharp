using Microsoft.Extensions.Logging;
using MonopolyKata.Spaces;

using MonopolyKata.Strategies;

namespace MonopolyKata
{
    public class Player
    {
        private readonly ILogger<Player>? _logger;
        public string Name { get; set; }
        public IPlayerPersonality Personality { get; }
        private int _bank = 0;
        public int Bank
        {
            get
            {
                return _bank;
            }
            set
            {
                _bank = value;
                _logger?.LogDebug($"{Name}'s bank balance is now ${Bank}.");
            }
        }
        public int Position { get; set; }
        public Board? BoardRef { get; set; }
        public Monopoly? GameRef { get; set; }
        public int Rounds { get; set; }
        public (int, int) LastRoll { get; set; }
        public bool IsInJail { get; set; }
        public int NumTurnsInJail { get; set; }
        public bool WantsToPayToGetOutOfJail { get; set; }
        public List<Property> Properties { get; set; }

        public Player(string name, IPlayerPersonality personality, ILogger<Player> logger)
        {
            Name = name;
            Personality = personality;
            Bank = 0;
            Position = 0;
            Rounds = 0;
            LastRoll = (0, 0);
            IsInJail = false;
            NumTurnsInJail = 0;
            WantsToPayToGetOutOfJail = false;
            Properties = [];
            _logger = logger;
        }

        public bool WantsToBuy(Property property)
            => Personality.ShouldBuy(this, property);

        public int GetNumberOfPropertiesOwnedInGroup(PropertyGroup group)
        {
            int numProperties = Properties.Count(property => property.Group == group);
            return numProperties;
        }

        public bool HasMonopoly(PropertyGroup group)
        {
            int numPropsInGroup = group.Properties.Count;
            int numPropsOwnedInGroup = GetNumberOfPropertiesOwnedInGroup(group);
            return numPropsInGroup == numPropsOwnedInGroup;
        }

        public void Build()
        {
            List<PropertyGroup> candidates =
                [.. Properties.Select(g => g.Group)
                            .Distinct()
                            .OrderBy(g => g.CostOfHouse)];

            foreach (PropertyGroup group in candidates)
                group.BuyProperties(this);
        }

        public void SendToJail()
        {
            IsInJail = true;
            NumTurnsInJail = 0;
            WantsToPayToGetOutOfJail = false;
            Position = BoardRef?.GetBoardPositionOfSpace("Jail") ?? 0;
        }

        public void Bankrupt()
        {
            foreach (Property prop in Properties)
            {
                prop.Reset();
            }
        }
    }
}
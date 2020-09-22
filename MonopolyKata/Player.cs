using MonopolyKata.Spaces;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class Player
    {
        public string Name { get; set; }
        public int Bank { get; set; }
        public int Position { get; set; }
        public int Rounds { get; set; }
        public int LastRoll { get; set; }
        public bool IsInJail { get; set; }
        public List<Property> Properties { get; set; }

        public Player(string name)
        {
            Name = name;
            Bank = 0;
            Position = 0;
            Rounds = 0;
            LastRoll = 0;
            IsInJail = false;
            Properties = new List<Property>();
        }

        public int GetNumberOfPropertiesOwnedInGroup(PropertyGroup group)
        {
            int numProperties = Properties.Count(property => property.Group == group);
            return numProperties;
        }
    }
}
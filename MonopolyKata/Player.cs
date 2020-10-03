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
        public (int, int) LastRoll { get; set; }
        public bool IsInJail { get; set; }
        public int NumTurnsInJail { get; set; }
        public bool WantsToPayToGetOutOfJail { get; set; }
        public List<Property> Properties { get; set; }

        public Player(string name)
        {
            Name = name;
            Bank = 0;
            Position = 0;
            Rounds = 0;
            LastRoll = (0, 0);
            IsInJail = false;
            NumTurnsInJail = 0;
            WantsToPayToGetOutOfJail = false;
            Properties = new List<Property>();
        }

        public int GetNumberOfPropertiesOwnedInGroup(PropertyGroup group)
        {
            int numProperties = Properties.Count(property => property.Group == group);
            return numProperties;
        }

        public bool CanExitJail()
        {
            if ( ! IsInJail )
                return true;
            
            if ( LastRoll.Item1 == LastRoll.Item2 )
                return true;
 
            if ( WantsToPayToGetOutOfJail )
            {
                Bank -= 50;
                return true;
            }

            if ( NumTurnsInJail == 3 )
            {
                Bank -= 50;
                return true;
            }

            return false;
        }
    }
}
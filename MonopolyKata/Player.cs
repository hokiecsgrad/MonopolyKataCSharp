using MonopolyKata.Spaces;
using System.Collections.Generic;

namespace MonopolyKata
{
    public class Player
    {
        public string Name { get; set; }
        public int Bank { get; set; }
        public int Position { get; set; }
        public int Rounds { get; set; }
        public int LastRoll { get; set; }
        public List<Space> Properties { get; set; }


        public Player(string name)
        {
            Name = name;
            Bank = 0;
            Position = 0;
            Rounds = 0;
            LastRoll = 0;
            Properties = new List<Space>();
        }
    }
}
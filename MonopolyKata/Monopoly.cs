using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class Monopoly
    {
        public List<Player> Players { get; set; } = new List<Player>();

        public Monopoly(List<Player> players)
        {
            Random random = new Random();
            Players = players.OrderBy(a => random.Next()).ToList();
        }

        public void Play()
        {
            if (Players.Count() < 2 || Players.Count() > 8)
                throw new InvalidOperationException("You must have at least 2 players and no more than 8 players!");
        }
    }
}
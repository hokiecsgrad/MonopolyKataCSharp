using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class Monopoly
    {
        public List<Player> Players { get; set; } = new List<Player>();
        public int Rounds = 0;

        private const int MaxRounds = 20;
        private Random RandomGenerator;

        public Monopoly(List<Player> players)
        {
            RandomGenerator = new Random();
            Players = RandomizePlayerOrder(players);

            Rounds = 0;
        }

        private List<Player> RandomizePlayerOrder(List<Player> players)
        {
            return players.OrderBy(a => RandomGenerator.Next()).ToList();
        }

        public void Start()
        {
            if (Players.Count() < 2 || Players.Count() > 8)
                throw new InvalidOperationException("You must have at least 2 players and no more than 8 players!");

            for (int currentRound = 0; currentRound < MaxRounds; currentRound++)
                PlayRound();
        }

        private void PlayRound()
        {
            foreach (Player player in Players)
            {
                TakeTurn(player);
            }
            Rounds++;
        }

        private void TakeTurn(Player player)
        {
            player.Rounds++;
        }
    }
}
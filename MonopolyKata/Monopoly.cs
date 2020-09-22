using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class Monopoly
    {
        public Board Board { get; }
        public Dice Dice { get; }
        public List<Player> Players { get; }
        public int Rounds = 0;

        private const int MaxRounds = 20;
        private Random RandomGenerator = new Random();
        private Turn Turn;

        public Monopoly(Board board, Dice dice, List<Player> players)
        {
            Board = board;
            Dice = dice;
            Turn = new Turn(Board, Dice);
            Players = RandomizePlayerOrder(players);

            foreach (Player player in Players)
                Board.AddPlayerToBoard(player, 0);
        }

        public Monopoly(List<Player> players)  
            : this(new MonopolyBoard(), new Dice(6), players)
        { }

        private List<Player> RandomizePlayerOrder(List<Player> players)
        {
            return players?.OrderBy(a => RandomGenerator.Next()).ToList();
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
                Turn.Take(player);
            }
            Rounds++;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class Monopoly
    {
        private IBoard Board { get; }
        private IDice Dice { get; }
        private ITurn Turn { get; }

        private const int MaxRounds = 20;
        private Random RandomGenerator = new Random();

        public List<Player> Players { get; private set; }
        public int Rounds = 0;

        public Monopoly(IBoard board, IDice dice, ITurn turn)
        {
            Board = board;
            Dice = dice;
            Turn = turn;
            Players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void Start()
        {
            if (Players.Count() < 2 || Players.Count() > 8)
                throw new InvalidOperationException("You must have at least 2 players and no more than 8 players!");

            Players = RandomizePlayerOrder(Players);
            foreach (Player player in Players)
                Board.AddPlayerToBoard(player, 0);

            for (int currentRound = 0; currentRound < MaxRounds; currentRound++)
                PlayRound();
        }

        private List<Player> RandomizePlayerOrder(List<Player> players)
        {
            return players?.OrderBy(a => RandomGenerator.Next()).ToList();
        }

        private void PlayRound()
        {
            foreach (Player player in Players)
            {
                Turn.Take(player);
            }
            Rounds++;
        }

        public Player GetWinner()
        {
            return Players.OrderByDescending(player => player.Bank).First();
        }
    }
}
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class Monopoly
    {
        private readonly ILogger<Monopoly> _logger = null;
        private IBoard Board { get; }
        private IDice Dice { get; }
        private ITurn Turn { get; }

        private const int MaxRounds = 200;
        private Random RandomGenerator = new Random();

        public List<Player> Players { get; private set; }
        public int Rounds = 0;

        public Monopoly(IBoard board, IDice dice, ITurn turn, ILoggerFactory loggerFactory = null)
        {
            Board = board;
            Dice = dice;
            Turn = turn;
            Players = new List<Player>();
            _logger = loggerFactory?.CreateLogger<Monopoly>();
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void Start()
        {
            SetupGame();
            PlayGame();
        }

        private void SetupGame()
        {
            ValidatePlayers();
            RandomizePlayerOrder();
            AddPlayersToStartingPosition();
        }

            private void ValidatePlayers()
            {
                if (Players.Count() < 2 || Players.Count() > 8)
                    throw new InvalidOperationException("You must have at least 2 players and no more than 8 players!");
            }

            private void RandomizePlayerOrder()
            {
                Players =  Players?.OrderBy(a => RandomGenerator.Next()).ToList();
            }

            private void AddPlayersToStartingPosition()
            {
                foreach (Player player in Players)
                    Board?.AddPlayerToBoard(player, 0);
            }

        private void PlayGame()
        {
            while (Players.Count() > 1 && Rounds < MaxRounds)
                PlayRound();

            Player winner = GetWinner();

            _logger?.LogInformation("");
            _logger?.LogInformation("The players played a total of {0} rounds.", Rounds);
            _logger?.LogInformation("{0} wins the game with ${1} in the bank!", winner.Name, winner.Bank);
        }

        private void PlayRound()
        {
            foreach (Player player in Players)
            {
                Turn?.Take(player);
                player.Rounds++;
            }

            RemoveBankruptPlayers();

            Rounds++;
        }

        private void RemoveBankruptPlayers()
        {
            List<Player> bankruptedPlayers = Players.FindAll(player => player.Bank < 0).ToList();
            foreach (Player loser in bankruptedPlayers)
            {
                loser.Bankrupt();
                Players.Remove(loser);
                _logger?.LogInformation("");
                _logger?.LogInformation("{0} has gone bankrupt and is now out of the game.", loser.Name);
            }
        }

        public Player GetWinner()
        {
            return Players.OrderByDescending(player => player.Bank).First();
        }
    }
}
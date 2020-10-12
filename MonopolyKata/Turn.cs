using Microsoft.Extensions.Logging;

namespace MonopolyKata
{
    public class Turn : ITurn
    {
        private readonly ILogger<Turn> _logger = null;
        private IBoard Board { get; }
        private IDice Dice { get; set; }

        public Turn(IBoard board, IDice dice, ILoggerFactory loggerFactory = null)
        {
            Board = board;
            Dice = dice;
            _logger = loggerFactory?.CreateLogger<Turn>();
        }

        public void Take(Player player)
        {
            (int, int) roll;
            int numberOfRolls = 0;

            _logger?.LogInformation("");
            _logger?.LogInformation("{0} starts their turn.", player.Name);

            if (player.IsInJail) player.NumTurnsInJail++;

            do
            {
                numberOfRolls += 1;
                roll = Dice.Roll();
                player.LastRoll = roll;

                _logger?.LogInformation("{0} has rolled {1}, a {2} and a {3}.", 
                    player.Name, 
                    roll.Item1 + roll.Item2, 
                    roll.Item1, 
                    roll.Item2);

                if (!player.IsInJail && Dice.LastRollWasDoubles && numberOfRolls == 3)
                {
                    player.SendToJail();
                    _logger?.LogInformation("{0} was sent to Jail for rolling doubles 3 times in a row.", player.Name);
                    break;
                }

                Board.Move(player, roll.Item1 + roll.Item2);

            } while (!player.IsInJail && player.NumTurnsInJail == 0 && Dice.LastRollWasDoubles && numberOfRolls < 3);

            if (!player.IsInJail) { player.NumTurnsInJail = 0; player.WantsToPayToGetOutOfJail = false; }

            player.Rounds++;

            _logger?.LogInformation("{0} ends their turn.", player.Name);
        }
    }
}
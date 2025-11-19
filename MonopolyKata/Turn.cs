using Microsoft.Extensions.Logging;

namespace MonopolyKata
{
    public class Turn(IBoard board, IDice dice, ILoggerFactory loggerFactory) : ITurn
    {
        private readonly ILogger<Turn>? _logger = loggerFactory?.CreateLogger<Turn>();
        private IBoard Board { get; } = board;
        private IDice Dice { get; set; } = dice;

        public void SetDice(Dice dice)
        {
            Dice = dice;
        }

        public void Take(Player player)
        {
            (int, int) roll;
            int numberOfRolls = 0;

            _logger?.LogInformation("");
            _logger?.LogInformation($"{player.Name} starts their turn.");

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

                if ( HasRolledDoubles3TimesInARow(player, numberOfRolls) ) 
                {
                    player.SendToJail();
                    _logger?.LogInformation($"{player.Name} was sent to Jail for rolling doubles 3 times in a row.");
                    break;
                }

                if ( player.IsInJail && TryExitJail(player) ) 
                {
                    Board.Move(player, roll.Item1 + roll.Item2);
                    break;
                }
                else if ( player.IsInJail )
                    break;

                Board.Move(player, roll.Item1 + roll.Item2);

                player.Build();

            } while (Dice.LastRollWasDoubles && !player.IsInJail);

            _logger?.LogInformation($"{player.Name} ends their turn.");
        }

        private bool HasRolledDoubles3TimesInARow(Player player, int numberOfRolls)
            => (!player.IsInJail && Dice.LastRollWasDoubles && numberOfRolls >= 3);

        private bool TryExitJail(Player player)
        {
            player.NumTurnsInJail++;

            if ( CanExitJail(player) )
            {
                player.IsInJail = false;
                player.NumTurnsInJail = 0;
                return true;
            }
            else
            {
                _logger?.LogInformation($"{player.Name} cannot exit Jail.");
                return false;
            }
        }

        private bool CanExitJail(Player player)
        {
            if ( ! player.IsInJail ) return true;

            if ( Dice.LastRollWasDoubles )
            {
                _logger?.LogInformation($"{player.Name} rolled doubles and can now leave Jail.");
                return true;
            }

            if ( player.WantsToPayToGetOutOfJail || player.NumTurnsInJail == 3 )
            {
                player.Bank -= 50;
                _logger?.LogInformation($"{player.Name} has paid $50 to leave Jail.");
                return true;
            }
 
            return false;
        }
    }
}
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

        public void SetDice(Dice dice)
        {
            Dice = dice;
        }

        public void Take(Player player)
        {
            (int, int) roll;
            int numberOfRolls = 0;

            _logger?.LogInformation("");
            _logger?.LogInformation("{0} starts their turn.", player.Name);

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
                    _logger?.LogInformation("{0} was sent to Jail for rolling doubles 3 times in a row.", player.Name);
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

            _logger?.LogInformation("{0} ends their turn.", player.Name);
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
                _logger?.LogInformation("{0} cannot exit Jail.", player.Name);
                return false;
            }
        }

        private bool CanExitJail(Player player)
        {
            if ( ! player.IsInJail ) return true;

            if ( Dice.LastRollWasDoubles )
            {
                _logger?.LogInformation("{0} rolled doubles and can now leave Jail.", player.Name);
                return true;
            }

            if ( player.WantsToPayToGetOutOfJail || player.NumTurnsInJail == 3 )
            {
                player.Bank -= 50;
                _logger?.LogInformation("{0} has paid $50 to leave Jail.", player.Name);
                return true;
            }
 
            return false;
        }
    }
}
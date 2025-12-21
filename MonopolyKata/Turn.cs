using Microsoft.Extensions.Logging;

namespace MonopolyKata
{
    public class Turn(IBoard board, IDice dice, ILoggerFactory? loggerFactory = null) : ITurn
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
            _logger?.LogInformation("");
            _logger?.LogInformation($"{player.Name} starts their turn.");

            if (player.IsInJail)
                HandleJailTurn(player);
            else
                HandleNormalTurn(player);

            player.Build();

            _logger?.LogInformation($"{player.Name} ends their turn.");
        }

        private void HandleNormalTurn(Player player)
        {
            int consecutiveDoubles = 0;

            do
            {
                var roll = Dice.Roll();
                player.LastRoll = roll;
                int total = roll.Item1 + roll.Item2;

                _logger?.LogInformation("{Name} rolled {Total} ({D1} + {D2}).",
                    player.Name, total, roll.Item1, roll.Item2);

                if (Dice.LastRollWasDoubles)
                {
                    consecutiveDoubles++;

                    if (consecutiveDoubles >= 3)
                    {
                        _logger?.LogInformation("{Name} rolled doubles 3 times and goes to Jail!", player.Name);
                        player.SendToJail();
                        return;
                    }
                }

                Board.Move(player, total);

            } while (Dice.LastRollWasDoubles && !player.IsInJail);
        }

        private void HandleJailTurn(Player player)
        {
            player.NumTurnsInJail++;

            // 1. Ask the Strategy: Do they want to pay to leave?
            // (We add a new method to the Strategy interface for this)
            bool wantsToPay = player.WantsToPayBail();

            if (wantsToPay || player.NumTurnsInJail >= 3)
            {
                PayBail(player);
                // If they pay, they roll and move normally this turn
                HandleNormalTurn(player);
                return;
            }

            // 2. Try to roll doubles
            var roll = Dice.Roll();
            _logger?.LogInformation("{Name} attempts to roll doubles to leave jail: {D1} + {D2}",
                player.Name, roll.Item1, roll.Item2);

            if (Dice.LastRollWasDoubles)
            {
                _logger?.LogInformation("Doubles! {Name} is free!", player.Name);
                player.ReleaseFromJail();

                // Standard rules: If you roll doubles to escape, you move that amount, 
                // but you do NOT get a "doubles" bonus roll.
                Board.Move(player, roll.Item1 + roll.Item2);
            }
            else
            {
                _logger?.LogInformation("{Name} stays in Jail.", player.Name);
            }
        }

        private void PayBail(Player player)
        {
            player.Bank -= 50;
            player.ReleaseFromJail();
            _logger?.LogInformation("{Name} paid $50 to leave Jail.", player.Name);
        }
    }
}